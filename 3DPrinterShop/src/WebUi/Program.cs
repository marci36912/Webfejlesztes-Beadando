using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using PrinterShop.WebUi;
using PrinterShop.WebUi.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var settings = new RefitSettings()
{
    ContentSerializer = new SystemTextJsonContentSerializer(),
};

builder.Services.AddRefitClient<IUserService>(settings)
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5001/"));

await builder.Build().RunAsync();