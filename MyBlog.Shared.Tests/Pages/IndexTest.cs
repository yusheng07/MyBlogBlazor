using MyBlog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Shared.Tests.Pages
{
    public class IndexTest : TestContext
    {
        public IndexTest()
        {
            Services.AddScoped<IMyBlogApi, MyBlogApiMock>();
        }

        [Fact(DisplayName = "Shows 10 blog posts")]
        public void Shows10Blogposts()
        {
            var cut = RenderComponent<MyBlog.Shared.Pages.Index>();
            Assert.Equal(10, cut.FindAll("article").Count());
        }

    }
}
