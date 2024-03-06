using Blazorise;
using Blazorise.Icons.FontAwesome;
using Blazorise.Tailwind;
using FleetManagement;
using FleetManagement.ClientServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazorise(options =>
{
    options.Immediate = true;
})
    .AddTailwindProviders()
    .AddFontAwesomeIcons();

builder.Services.AddScoped<IVehicleServices, VehicleServices>();

await builder.Build().RunAsync();
