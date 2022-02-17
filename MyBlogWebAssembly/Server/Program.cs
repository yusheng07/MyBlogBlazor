using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Data.Interfaces;

//<IdentityServerUsing>
using MyBlog.Data.Models;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
//<IdentityServerUsing>

using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//<AddMyBlogDataServices>
builder.Services.AddDbContextFactory<MyBlogDbContext>(options =>
                        options.UseSqlite(builder.Configuration.GetConnectionString("MyBlogDB")));
builder.Services.AddScoped<IMyBlogApi, MyBlogApiServerSide>();
//<AddMyBlogDataServices>


//<IdentityServer>
builder.Services.AddDbContext<MyBlogDbContext>(options =>
                        options.UseSqlite(builder.Configuration.GetConnectionString("MyBlogDB")));
//configure the Identity provider
//  add roles to Services.AddDefaultIdentity
builder.Services.AddDefaultIdentity<AppUser>(options =>
                        options.SignIn.RequireConfirmedAccount = false)
                //the server will send roles over the client
                .AddRoles<IdentityRole>()   
                .AddEntityFrameworkStores<MyBlogDbContext>();
//configure the IdentityServer
builder.Services.AddIdentityServer()
                .AddApiAuthorization<AppUser, MyBlogDbContext>(options =>
                {
                    //add options to AddApiAuthorization
                    //  include roles in the token so that can use the token on the client
                    options.IdentityResources["openid"].UserClaims.Add("name");
                    options.ApiResources.Single().UserClaims.Add("name");
                    options.IdentityResources["openid"].UserClaims.Add("role");
                    options.ApiResources.Single().UserClaims.Add("role");
                });
//remove the default claim mapping
JwtSecurityTokenHandler.DefaultInboundClaimFilter.Remove("role");
//add a JWT to the configuration
//  one showing that we are logged in and one that we use for API access
builder.Services.AddAuthentication().AddIdentityServerJwt();
//<IdentityServer>


builder.Services.AddControllersWithViews();
                //.AddJsonOptions(options => 
                //{
                //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                //});
builder.Services.AddRazorPages();

// Set the JSON serializer options
builder.Services.Configure<System.Text.Json.JsonSerializerOptions>(options =>
{
    options.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.PropertyNamingPolicy = null;
});

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
