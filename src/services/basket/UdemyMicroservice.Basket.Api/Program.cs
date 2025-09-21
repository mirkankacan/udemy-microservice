using Asp.Versioning.Builder;
using Mapster;
using System.Reflection;
using UdemyMicroservice.Basket.Api;
using UdemyMicroservice.Basket.Api.Features.Baskets;
using UdemyMicroservice.Basket.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExtension(typeof(BasketAssembly));
builder.Services.AddVersioningExtension();
builder.Services.AddStackExchangeRedisCache(opts =>
{
    opts.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddScoped<IBasketService, BasketService>();
var config = TypeAdapterConfig.GlobalSettings;
config.Scan(Assembly.GetExecutingAssembly());

var app = builder.Build();

ApiVersionSet version = app.AddVersionSetExtension();
app.AddBasketGroupEndpointExtension(version);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();