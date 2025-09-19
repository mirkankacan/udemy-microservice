using Asp.Versioning.Builder;
using UdemyMicroservice.Catalog.Api.Features.Categories.GetAll;
using UdemyMicroservice.Discount.Api.Features.Discounts.Create;
using UdemyMicroservice.Discount.Api.Features.Discounts.Delete;
using UdemyMicroservice.Discount.Api.Features.Discounts.GetById;
using UdemyMicroservice.Discount.Api.Features.Discounts.Update;

namespace UdemyMicroservice.Discount.Api.Features.Discounts
{
    public static class DiscountEndpointExtension
    {
        public static void AddDiscountGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/discounts")
                .WithApiVersionSet(apiVersionSet)
                .WithTags("Discounts")
                .WithDescription("Discount management endpoints for creating, reading, updating, and deleting discount")
                .CreateDiscountGroupItemEndpoint()
                .GetAllDiscountGroupItemEndpoint()
                .GetDiscountByIdGroupItemEndpoint()
                .DeleteDiscountGroupItemEndpoint()
                .UpdateDiscountGroupItemEndpoint()
                /*.RequireAuthorization()*/;
        }
    }
}