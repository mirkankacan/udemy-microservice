using Asp.Versioning.Builder;
using UdemyMicroservice.Catalog.Api;
using UdemyMicroservice.Catalog.Api.Data.Extensions;
using UdemyMicroservice.Catalog.Api.Features.Categories;
using UdemyMicroservice.Catalog.Api.Features.Courses;
using UdemyMicroservice.Catalog.Api.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExtensions();
builder.Services.AddDataExtensions();
builder.Services.AddCommonServiceExtension(typeof(CatalogAssembly));
builder.Services.AddVersioningExtension();

var app = builder.Build();

app.AddSeedDataExtension().ContinueWith(task =>
{
    Console.WriteLine(task.IsFaulted ? "An error occurred while seeding the database: " + task.Exception?.Message : "Database seeding completed successfully");
});

ApiVersionSet version = app.AddVersionSetExtension();
app.AddCategoryGroupEndpointExtension(version);
app.AddCourseGroupEndpointExtension(version);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.Run();