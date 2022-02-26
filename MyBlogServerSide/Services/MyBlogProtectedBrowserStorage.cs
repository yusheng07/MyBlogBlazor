using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MyBlog.Shared.Interfaces;

namespace MyBlogServerSide.Services
{
    public class MyBlogProtectedBrowserStorage : IBrowserStorage
    {
        ProtectedSessionStorage Storage { get; set; }

        public MyBlogProtectedBrowserStorage(ProtectedSessionStorage storage)
        {
            Storage = storage;
        }

        public MyBlogProtectedBrowserStorage()
        {
            Storage = null;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var storageVal = await Storage.GetAsync<T>(key);
            if (storageVal.Success)
            {
                return storageVal.Value;
            }
            else
            {
                return default(T);
            }
        }

        public async Task SetAsync(string key, object value)
        {
            await Storage.SetAsync(key, value);
        }

        public async Task DeleteAsync(string key)
        {
            await Storage.DeleteAsync(key);
        }
    }
}
