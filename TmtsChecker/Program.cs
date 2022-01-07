using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TmtsChecker.Data;
using MudBlazor.Services;
using MudBlazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices(config =>
    {
        config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
        config.SnackbarConfiguration.VisibleStateDuration = 3000;
        //config.SnackbarConfiguration.PreventDuplicates = false;
        //config.SnackbarConfiguration.NewestOnTop = false;
        //config.SnackbarConfiguration.ShowCloseIcon = true;
        //config.SnackbarConfiguration.HideTransitionDuration = 500;
        //config.SnackbarConfiguration.ShowTransitionDuration = 500;
        //config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
    });
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<IScanResultService,TmdResultService>();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
