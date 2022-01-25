using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MyBlog.Data.Extensions;
using MyBlog.Data.Interfaces;
using MyBlog.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data
{
    internal class MyBlogApiClientSide : IMyBlogApi
    {
        private readonly IHttpClientFactory factory;
        public MyBlogApiClientSide(IHttpClientFactory factory)
        {
            this.factory = factory;
        }

        #region BlogPost
        public async Task<BlogPost> GetBlogPostAsync(int id)
        {
            var httpClient = factory.CreateClient("Public");
            return await httpClient.GetFromJsonAsync<BlogPost>($"MyBlogApi/BlogPosts/{id}");
        }

        public async Task<int> GetBlogPostCountAsync()
        {
            var httpClient = factory.CreateClient("Public");
            return await httpClient.GetFromJsonAsync<int>("MyBlogApi/BlogPostCount");
        }

        public async Task<List<BlogPost>> GetBlogPostsAsync(int numberofposts, int startindex)
        {
            var httpClient = factory.CreateClient("Public");
            return await httpClient.GetFromJsonAsync<List<BlogPost>>($"MyBlogApi/BlogPosts?numberofposts={numberofposts}&startindex={startindex}");
        }

        public async Task<BlogPost> SaveBlogPostAsync(BlogPost item)
        {
            try
            {
                var httpClient = factory.CreateClient("Authenticated");
                var response = await httpClient.PutAsJsonAsync<BlogPost>("MyBlogApi/BlogPosts", item);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BlogPost>(json);
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
            return null;
        }

        public async Task DeleteBlogPostAsync(BlogPost item)
        {
            try
            {
                var httpClient = factory.CreateClient("Authenticated");
                await httpClient.DeleteAsJsonAsync<BlogPost>("MyBlogApi/BlogPosts", item);

            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }
        #endregion

        #region Category
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var httpClient = factory.CreateClient("Public");
            return await httpClient.GetFromJsonAsync<List<Category>>("MyBlogApi/Categories");
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            var httpClient = factory.CreateClient("Public");
            return await httpClient.GetFromJsonAsync<Category>($"MyBlogApi/Categories/{id}");
        }

        public async Task<Category> SaveCategoryAsync(Category item)
        {
            try
            {
                var httpClient = factory.CreateClient("Authenticated");
                var response = await httpClient.PutAsJsonAsync<Category>("MyBlogApi/Categories", item);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Category>(json);
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
            return null;
        }

        public async Task DeleteCategoryAsync(Category item)
        {
            try
            {
                var httpClient = factory.CreateClient("Authenticated");
                await httpClient.DeleteAsJsonAsync<Category>("MyBlogApi/Categories", item);

            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }
        #endregion

        #region Tag
        public async Task<List<Tag>> GetTagsAsync()
        {
            var httpClient = factory.CreateClient("Public");
            return await httpClient.GetFromJsonAsync<List<Tag>>("MyBlogApi/Tags");
        }

        public async Task<Tag> GetTagAsync(int id)
        {
            var httpClient = factory.CreateClient("Public");
            return await httpClient.GetFromJsonAsync<Tag>($"MyBlogApi/Tags/{id}");
        }

        public async Task<Tag> SaveTagAsync(Tag item)
        {
            try
            {
                var httpClient = factory.CreateClient("Authenticated");
                var response = await httpClient.PutAsJsonAsync<Tag>("MyBlogApi/Tags", item);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Tag>(json);
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
            return null;
        }

        public async Task DeleteTagAsync(Tag item)
        {
            try
            {
                var httpClient = factory.CreateClient("Authenticated");
                await httpClient.DeleteAsJsonAsync<Tag>("MyBlogApi/Tags", item);

            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        } 
        #endregion

    }
}
