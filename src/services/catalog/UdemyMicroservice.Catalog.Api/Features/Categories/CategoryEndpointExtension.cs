using Asp.Versioning.Builder;
using UdemyMicroservice.Catalog.Api.Features.Categories.Create;
using UdemyMicroservice.Catalog.Api.Features.Categories.Delete;
using UdemyMicroservice.Catalog.Api.Features.Categories.GetAll;
using UdemyMicroservice.Catalog.Api.Features.Categories.GetById;
using UdemyMicroservice.Catalog.Api.Features.Categories.Update;

namespace UdemyMicroservice.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExtension
    {
        public static void AddCategoryGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/categories")
                .WithApiVersionSet(apiVersionSet)
                .WithTags("Categories")
                .WithDescription("Category management endpoints for creating, reading, updating, and deleting category")
                .CreateCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint()
                .GetCategoryByIdGroupItemEndpoint()
                .DeleteCategoryGroupItemEndpoint()
                .UpdateCategoryGroupItemEndpoint()
                /*.RequireAuthorization()*/;
        }
    }
}