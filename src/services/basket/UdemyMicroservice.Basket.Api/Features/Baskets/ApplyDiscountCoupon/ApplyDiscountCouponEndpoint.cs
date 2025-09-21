using UdemyMicroservice.Shared.Filters;

namespace UdemyMicroservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public static class ApplyDiscountCouponEndpoint
    {
        public static RouteGroupBuilder ApplyDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/apply-discount", async (ApplyDiscountCouponCommand command, IMediator mediator) =>
            {
                return (await mediator.Send(command)).ToGenericResult();
            })
                .WithName("ApplyDiscountCoupon")
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommand>>();
            return group;
        }
    }
}