using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using MudBlazor;
using HashWorkerBlazor.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//<AddDbDataServices>
builder.Services.AddDbContextFactory<HashWorkerDbContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("HashWorkerDB")));
//builder.Services.AddDbContextFactory<HashWorkerDbContext>(options =>
//                        options.UseSqlite(builder.Configuration.GetConnectionString("HashWorkerDB")));
builder.Services.AddScoped<IHashWorkerApi, HashWorkerApi>();
//<AddDbDataServices>

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.VisibleStateDuration = 3000;
});
//builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

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


//<AutoCreateDatabase>
using (var scope = ((IApplicationBuilder)app).ApplicationServices.CreateScope())
{
    using (var context = scope.ServiceProvider.GetService<HashWorkerDbContext>())
    {
        //context?.Database.Migrate();
        context?.Database.EnsureCreated();
    }
}
//<AutoCreateDatabase>

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
