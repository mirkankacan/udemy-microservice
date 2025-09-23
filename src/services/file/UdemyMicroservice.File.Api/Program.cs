using Asp.Versioning.Builder;
using Microsoft.Extensions.FileProviders;
using UdemyMicroservice.Discount.Api.Features.Discounts;
using UdemyMicroservice.File.Api;
using UdemyMicroservice.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExtension(typeof(FileAssembly));
builder.Services.AddVersioningExtension();
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
builder.Services.AddAuthenticationAndAuthorizationExtension(builder.Configuration);

var app = builder.Build();

ApiVersionSet version = app.AddVersionSetExtension();
app.AddFileGroupEndpointExtension(version);

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