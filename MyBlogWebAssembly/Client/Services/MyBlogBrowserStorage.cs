using Blazored.SessionStorage;
using MyBlog.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogWebAssembly.Client.Services
{
    public class MyBlogBrowserStorage : IBrowserStorage
    {
        ISessionStorageService Storage { get; set; }

        public MyBlogBrowserStorage(ISessionStorageService storage)
        {
            Storage = storage;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await Storage.GetItemAsync<T>(key).AsTask();
        }

        public async Task SetAsync(string key, object value)
        {
            await Storage.SetItemAsync(key, value);
        }

        public async Task DeleteAsync(string key)
        {
            await Storage.RemoveItemAsync(key);
        }
    }
}
