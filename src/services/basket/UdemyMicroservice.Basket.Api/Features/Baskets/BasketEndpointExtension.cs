using Asp.Versioning.Builder;
using UdemyMicroservice.Basket.Api.Features.Baskets.AddItem;
using UdemyMicroservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using UdemyMicroservice.Basket.Api.Features.Baskets.DeleteItem;
using UdemyMicroservice.Basket.Api.Features.Baskets.GetByUser;
using UdemyMicroservice.Basket.Api.Features.Baskets.RemoveDiscountCoupon;

namespace UdemyMicroservice.Basket.Api.Features.Baskets
{
    public static class BasketEndpointExtension
    {
        public static void AddBasketGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets")
                .WithApiVersionSet(apiVersionSet)
                .WithTags("Baskets")
                .WithDescription("Basket management endpoints for adding, reading, and deleting basket, basket items")
                .AddBasketItemGroupItemEndpoint()
                .GetBasketByUserGroupItemEndpoint()
                .DeleteBasketItemGroupItemEndpoint()
                .ApplyDiscountCouponGroupItemEndpoint()
                .RemoveDiscountCouponGroupItemEndpoint()
                .RequireAuthorization();
        }
    }
}