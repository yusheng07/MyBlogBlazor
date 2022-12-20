using HashWorkerBlazor.Models;
using HashWorkerBlazor.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.IO.Compression;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Text.Json;
using static HashWorkerBlazor.Tools.EncryptHelper;
using static MudBlazor.CategoryTypes;
using ZipFile = Ionic.Zip.ZipFile;  //Nuget: DotNetZip Library 

namespace HashWorkerBlazor.DB
{
    public class HashWorkerApi : IHashWorkerApi
    {
        private IDbContextFactory<HashWorkerDbContext> factory;
        private string TransPath { get; set; }
        private string ZipPwd { get; set; }

        public HashWorkerApi(IDbContextFactory<HashWorkerDbContext> factory)
        {
            this.factory = factory;
            //
            var config = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json")
                          .Build();
            TransPath = config["AppVals:TransPath"];
            ZipPwd = config["AppVals:ZipPwd"];
        }

        public async Task<List<ListItem>> GetListItemsAsync()
        {
            using var context = factory.CreateDbContext();
            return await context.ListItems.OrderByDescending(item=>item.CreateTime).ToListAsync();
        }

        public async Task<(bool isOk, string msg)> ResendListAsync(int listIdx)
        {
            using var context = factory.CreateDbContext();
            var listItem = await context.ListItems.FirstOrDefaultAsync(m => m.Id== listIdx);
            if (listItem == null)
            {
                return ValueTuple.Create(false, $"no match item in DB! idx:{listIdx}");
            }

            //TODO: 加密壓縮至自動傳送目錄
            try
            {
                SendZipFile(listItem);
            }
            catch (Exception ex)
            {
                return ValueTuple.Create(false, $"send item failed! idx:{listIdx}, msg:{ex.Message}");
            }

            listItem.LastSendTime = DateTime.Now;
            await context.SaveChangesAsync();

            //return OK
            return ValueTuple.Create(true, $"send item completed! idx:{listItem.Id}");
        }

        public async Task<(bool isOk, string msg)> SendListAsync(string account, string hashListJson, string checkHash,int hashCount)
        {
            using var context = factory.CreateDbContext();

            //TODO: 重複性判斷
            var listItem = await context.ListItems.FirstOrDefaultAsync(m => m.CheckHash.Equals(checkHash));
            if (listItem != null)
            {
                //return ValueTuple.Create(false, $"duplicate folder and hashList! idx:{listItem.Id}");
            }

            //create new item
            var newListItem = await context.ListItems.AddAsync(
                new ListItem
                {
                    Account = account,
                    HashCount = hashCount,
                    CreateTime = DateTime.Now,
                    LastSendTime = DateTime.Now,
                    HashListJson = hashListJson,
                    CheckHash = checkHash
                });
            await context.SaveChangesAsync();

            //TODO: 加密壓縮至自動傳送目錄
            try
            {
                SendZipFile(newListItem.Entity);
            }
            catch (Exception ex)
            {
                return ValueTuple.Create(false, $"item created with idx:{newListItem.Entity.Id}, but send failed! msg:{ex.Message}");
            }

            //return OK
            return ValueTuple.Create(true, $"item created! idx:{newListItem.Entity.Id}");
        }

        private void SendZipFile(ListItem listItem)
        {
            using (var zip = new ZipFile())
            {
                string outFilePath = Path.Combine(TransPath, $"HashWorkerSync_{listItem.Id}_{DateTime.Now.ToString("yyyyMMddHHmm")}.zip");
                zip.Password = ZipPwd;
                zip.AddEntry("ListItem.txt", JsonSerializer.Serialize(listItem, new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping }));
                Console.Write("Save ZIP file...");
                zip.Save(outFilePath);
            }
        }
    }
}
