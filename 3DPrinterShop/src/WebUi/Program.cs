using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using WebUi;
using WebUi.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRefitClient<IUserService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("127.0.0.1"));

await builder.Build().RunAsync();