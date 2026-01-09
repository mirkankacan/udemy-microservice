using Asp.Versioning.Builder;
using Microsoft.EntityFrameworkCore;
using UdemyMicroservice.Bus.Extensions;
using UdemyMicroservice.Discount.Api.Features.Discounts;
using UdemyMicroservice.Order.Application;
using UdemyMicroservice.Order.Application.Contracts;
using UdemyMicroservice.Order.Application.Contracts.Repositories;
using UdemyMicroservice.Order.Application.Contracts.UnitOfWork;
using UdemyMicroservice.Order.Persistance.Data;
using UdemyMicroservice.Order.Persistance.Repositories;
using UdemyMicroservice.Order.Persistance.UnitOfWork;
using UdemyMicroservice.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExtension(typeof(OrderApplicationAssembly));
builder.Services.AddMassTransitCommonExtension(builder.Configuration);
builder.Services.AddVersioningExtension();

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAuthenticationAndAuthorizationExtension(builder.Configuration);

var app = builder.Build();

ApiVersionSet version = app.AddVersionSetExtension();
app.AddOrderGroupEndpointExtension(version);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();
app.Run();