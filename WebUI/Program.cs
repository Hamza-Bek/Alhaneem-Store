using Application.Service;
using Application.Services;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebUI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IProductAdminService, ProductAdminService>();
builder.Services.AddScoped<IOrderAdminService, OrderAdminService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddHttpClient<IOrderAdminService, OrderAdminService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7201");
});

builder.Services.AddHttpClient<IOrderService, OrderService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7201");
});

builder.Services.AddHttpClient<IProductService, ProductService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7201");
});

builder.Services.AddHttpClient<ICartService, CartService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7201");
});

builder.Services.AddHttpClient<ILocationService, LocationService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7201");
});

builder.Services.AddHttpClient<IProductAdminService, ProductAdminService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7201");
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7201") });

await builder.Build().RunAsync();