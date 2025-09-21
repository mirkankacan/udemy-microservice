using Asp.Versioning.Builder;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UdemyMicroservice.Order.Persistance.Data;
using UdemyMicroservice.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddCommonServiceExtension(typeof(DiscountAssembly));
builder.Services.AddVersioningExtension();

var config = TypeAdapterConfig.GlobalSettings;
config.Scan(Assembly.GetExecutingAssembly());

var app = builder.Build();

ApiVersionSet version = app.AddVersionSetExtension();
//app.AddDiscountGroupEndpointExtension(version);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.Run();