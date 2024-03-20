using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using FleetManagement;
using FleetManagement.ClientServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazorise(options =>
{
    options.Immediate = true;
})
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.AddScoped<IVehicleServices, VehicleServices>();
builder.Services.AddScoped<IModelServices, ModelServices>();
builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();

//await builder.Build().RunAsync();

builder.Services.AddLocalization();
builder.Services.AddHttpClient<ServiceBase>(ServiceBase =>
{
    ServiceBase.BaseAddress = new Uri("https://localhost:7177");
});

var host = builder.Build();

CultureInfo culture;
var js = host.Services.GetRequiredService<IJSRuntime>();
var result = await js.InvokeAsync<string>("blazorCulture.get");

if (result != null)
{
    culture = new CultureInfo(result);
}
else
{
    culture = new CultureInfo("en-US");
    await js.InvokeVoidAsync("blazorCulture.set","en-US");
}

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();
