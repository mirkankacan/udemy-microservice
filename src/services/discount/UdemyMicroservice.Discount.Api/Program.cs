using Asp.Versioning.Builder;
using UdemyMicroservice.Discount.Api;
using UdemyMicroservice.Discount.Api.Data.Extensions;
using UdemyMicroservice.Discount.Api.Features.Discounts;
using UdemyMicroservice.Discount.Api.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExtensions();
builder.Services.AddDataExtensions();
builder.Services.AddCommonServiceExtension(typeof(DiscountAssembly));
builder.Services.AddVersioningExtension();
builder.Services.AddAuthenticationAndAuthorizationExtension(builder.Configuration);

var app = builder.Build();

app.AddSeedDataExtension().ContinueWith(task =>
{
    Console.WriteLine(task.IsFaulted ? "An error occurred while seeding the database: " + task.Exception?.Message : "Database seeding completed successfully");
});

ApiVersionSet version = app.AddVersionSetExtension();
app.AddDiscountGroupEndpointExtension(version);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.UseAuthentication();
app.UseAuthorization();
app.Run();