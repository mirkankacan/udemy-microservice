namespace UdemyMicroservice.Basket.Api.Features.Baskets.RemoveDiscountCoupon
{
    public static class RemoveDiscountCouponEndpoint
    {
        public static RouteGroupBuilder RemoveDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/remove-discount", async (IMediator mediator) =>
            {
                return (await mediator.Send(new RemoveDiscountCouponCommand())).ToGenericResult();
            })
                .WithName("RemoveDiscountCoupon")
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound);
            return group;
        }
    }
}