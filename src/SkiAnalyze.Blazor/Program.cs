using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SkiAnalyze.Blazor;
using Syncfusion.Blazor;
using SkiAnalyze.Blazor.Services;
using SkiAnalyze.Blazor.Services.Map;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTQyMDgwQDMxMzkyZTMzMmUzMFJ2a1lqNEFrK1lSTXVIR1h1N3MxYWZaeDVidy9iS0VzSEVMUDhoWFRuRU09");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ITracksService, TracksService>();
builder.Services.AddScoped<MapInterop>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();
builder.Services.AddSyncfusionBlazor();

await builder.Build().RunAsync();
