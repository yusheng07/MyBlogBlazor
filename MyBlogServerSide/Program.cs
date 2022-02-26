using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Data.Interfaces;
using MyBlog.Data.Models;
using MyBlog.Shared.Interfaces;
using MyBlogServerSide.Authentication;
using MyBlogServerSide.Data;
using MyBlogServerSide.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

//<AddMyBlogDataServices>
builder.Services.AddDbContextFactory<MyBlogDbContext>(options =>
                        options.UseSqlite(builder.Configuration.GetConnectionString("MyBlogDB")));
builder.Services.AddScoped<IMyBlogApi, MyBlogApiServerSide>();
//<AddMyBlogDataServices>

//<Authentication>
builder.Services.AddDbContext<MyBlogDbContext>(options =>
                        options.UseSqlite(builder.Configuration.GetConnectionString("MyBlogDB")));
builder.Services.AddDefaultIdentity<AppUser>(options =>
                        options.SignIn.RequireConfirmedAccount=true)
                //the server will send roles over the client
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MyBlogDbContext>();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<AppUser>>();
//<Authentication>

//<BrowserStorage>
builder.Services.AddScoped<IBrowserStorage, MyBlogProtectedBrowserStorage>();
//<BrowserStorage>


var app = builder.Build();

var factory = app.Services.GetRequiredService<IDbContextFactory<MyBlogDbContext>>();
factory.CreateDbContext().Database.Migrate();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
