using DemoBlazorServer.Data;
using DemoBlazorServer.Services.Authentication;
using DemoBlazorServer.Services.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddLocalizationLanguagesAndCulture();
builder.Services.AddGrpc();

builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped(provider => (AuthenticationStateProvider)provider.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddPizzeria();
builder.Services.AddViewModels();

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
app.CulturesRegistration();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

