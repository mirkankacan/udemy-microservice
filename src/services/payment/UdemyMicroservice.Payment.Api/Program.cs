using Asp.Versioning.Builder;
using Microsoft.EntityFrameworkCore;
using UdemyMicroservice.Discount.Api.Features.Discounts;
using UdemyMicroservice.Payment.Api;
using UdemyMicroservice.Payment.Api.Data;
using UdemyMicroservice.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExtension(typeof(PaymentAssembly));
builder.Services.AddVersioningExtension();
builder.Services.AddAuthenticationAndAuthorizationExtension(builder.Configuration);
var app = builder.Build();

ApiVersionSet version = app.AddVersionSetExtension();
app.AddPaymentGroupEndpointExtension(version);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.Run();