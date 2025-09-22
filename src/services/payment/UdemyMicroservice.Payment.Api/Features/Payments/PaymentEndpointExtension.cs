using Asp.Versioning.Builder;
using UdemyMicroservice.Payment.Api.Features.Payments.Create;
using UdemyMicroservice.Payment.Api.Features.Payments.GetByUser;

namespace UdemyMicroservice.Discount.Api.Features.Discounts
{
    public static class PaymentEndpointExtension
    {
        public static void AddPaymentGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/payments")
                .WithApiVersionSet(apiVersionSet)
                .WithTags("Payments")
                .WithDescription("Payment management endpoints for creating and reading payments")
                .CreatePaymentGroupItemEndpoint()
                .GetPaymentsByUserGroupItemEndpoint()
                /*.RequireAuthorization()*/;
        }
    }
}