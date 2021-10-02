using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Lager;
using Lager.Shared;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IJSInProcessRuntime>(services => (IJSInProcessRuntime)services.GetRequiredService<IJSRuntime>());
builder.Services.AddSingleton<LocalForage>();

await builder.Build().RunAsync();
