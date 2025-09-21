using Asp.Versioning.Builder;
using UdemyMicroservice.Order.Api.Endpoints;

namespace UdemyMicroservice.Discount.Api.Features.Discounts
{
    public static class OrderEndpointExtension
    {
        public static void AddOrderGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/orders")
                .WithApiVersionSet(apiVersionSet)
                .WithTags("Orders")
                .WithDescription("Order management endpoints for creating, reading, updating, and deleting orders")
                .CreateOrderGroupItemEndpoint()
                .GetOrdersByBuyerGroupItemEndpoint()
                /*.RequireAuthorization()*/;
        }
    }
}