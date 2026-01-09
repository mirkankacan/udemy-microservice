using Asp.Versioning.Builder;
using UdemyMicroservice.Basket.Api;
using UdemyMicroservice.Basket.Api.Features.Baskets;
using UdemyMicroservice.Basket.Api.Services;
using UdemyMicroservice.Bus.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExtension(typeof(BasketAssembly));
builder.Services.AddMassTransitCommonExtension(builder.Configuration, typeof(BasketAssembly));
builder.Services.AddVersioningExtension();
builder.Services.AddStackExchangeRedisCache(opts =>
{
    opts.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddAuthenticationAndAuthorizationExtension(builder.Configuration);

var app = builder.Build();

ApiVersionSet version = app.AddVersionSetExtension();
app.AddBasketGroupEndpointExtension(version);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.UseAuthentication();
app.UseAuthorization();
app.Run();