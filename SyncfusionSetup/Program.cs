using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SyncfusionSetup.Data;
using Syncfusion.Blazor;

/*
 * https://blazor.syncfusion.com/documentation/textbox/getting-started
    1. NuGet install Syncfusion Blazor Component 
    2. Open ~/_Imports.razor file and import the Syncfusion.Blazor namespace
    3. Open ~/Program.cs file and register the Syncfusion Blazor Service
    4. Open ~/Pages/_Layout.cshtml file and add the Syncfusion bootstrap5 theme in the <head>
    5. Open ~/Pages/_Layout.cshtml file and Refer script in the <head>

    3.Add Style Sheet. Blazor Server App.
    4.Add Script Reference. Blazor Server App.
    5.Add Blazor TextBox component.
    6.Adding icons to the TextBox.
 */

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//<AddSyncfusion>
builder.Services.AddSyncfusionBlazor();
//<AddSyncfusion>

builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

//Register Syncfusion license
/*
 * https://help.syncfusion.com/common/essential-studio/licensing/overview
 */
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzgyMTQwQDMyMzAyZTMzMmUzMGMxcERnUUtIM1BmYU40L3F3YlpHYmJxWldPaXRDeGswcGFCaC9pSzNIYzQ9");

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
