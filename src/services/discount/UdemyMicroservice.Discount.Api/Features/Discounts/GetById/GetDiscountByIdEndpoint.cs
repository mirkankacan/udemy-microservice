namespace UdemyMicroservice.Discount.Api.Features.Discounts.GetById
{
    public static class GetDiscountByCodeEndpoint
    {
        public static RouteGroupBuilder GetDiscountByIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                return (await mediator.Send(new GetDiscountByCodeQuery(id))).ToGenericResult();
            }).WithName("GetDiscountById")
            .MapToApiVersion(1, 0)
              .Produces<DiscountDto>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status404NotFound);
            return group;
        }
    }
}