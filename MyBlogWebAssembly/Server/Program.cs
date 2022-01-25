using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Data.Interfaces;
using MyBlog.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//<AddMyBlogDataServices>
builder.Services.AddDbContextFactory<MyBlogDbContext>(options =>
                        options.UseSqlite(builder.Configuration.GetConnectionString("MyBlogDB")));
builder.Services.AddScoped<IMyBlogApi, MyBlogApiServerSide>();
//<AddMyBlogDataServices>

//<Identity>
builder.Services.AddDbContext<MyBlogDbContext>(options =>
                        options.UseSqlite(builder.Configuration.GetConnectionString("MyBlogDB")));
builder.Services.AddDefaultIdentity<AppUser>(options =>
                        options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<MyBlogDbContext>();
builder.Services.AddIdentityServer().AddApiAuthorization<AppUser,MyBlogDbContext>();
builder.Services.AddAuthentication().AddIdentityServerJwt();
//<Identity>

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

//<IdentityApp>
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
//</IdentityApp>

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
