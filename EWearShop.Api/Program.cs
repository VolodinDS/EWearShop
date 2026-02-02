using EWearShop.Api.Features.Orders.CreateOrder;
using EWearShop.Api.Features.Orders.GetOrdersForAdmin;
using EWearShop.Api.Features.Products.GetProducts;
using EWearShop.DAL;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDataAccessLayer();
builder.Services.AddCors();

WebApplication app = builder.Build();

app.UseDataAccessLayer();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseStaticFiles();

app.MapGetProductsEndpoint();
app.MapGetOrdersForAdminEndpoint();
app.MapCreateOrderEndpoint();

app.UseHttpsRedirection();

await app.RunAsync();