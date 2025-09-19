using MediatR;

namespace UdemyMicroservice.Discount.Api.Features.Discounts.GetByCode
{
    public static class GetDiscountByCodeEndpoint
    {
        public static RouteGroupBuilder GetDiscountByCodeGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/code/{code:minlength(3):maxlength(10):required}", async (string code, IMediator mediator) =>
            {
                return (await mediator.Send(new GetDiscountByCodeQuery(code))).ToGenericResult();
            }).WithName("GetDiscountByCode")
            .MapToApiVersion(1, 0)
              .Produces<GetDiscountByCodeQueryResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status404NotFound);
            return group;
        }
    }
}