using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyBlog.Data;
using MyBlog.Data.Interfaces;
using MyBlogWebAssembly.Client;
using MyBlogWebAssembly.Client.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//<Identity>
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient("Authenticated",client=>client.BaseAddress= new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
builder.Services.AddHttpClient("Public", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddApiAuthorization()
                .AddAccountClaimsPrincipalFactory<RoleAccountClaimsPrincipalFactory>();
//<Identity>


builder.Services.AddScoped<IMyBlogApi, MyBlogApiClientSide>();

await builder.Build().RunAsync();
