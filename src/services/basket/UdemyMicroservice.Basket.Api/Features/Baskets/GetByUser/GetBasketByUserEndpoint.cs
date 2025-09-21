using UdemyMicroservice.Basket.Api.Features.Baskets.Dtos;

namespace UdemyMicroservice.Basket.Api.Features.Baskets.GetByUser
{
    public static class GetBasketByUserEndpoint
    {
        public static RouteGroupBuilder GetBasketByUserGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user", async (IMediator mediator) =>
            {
                return (await mediator.Send(new GetBasketByUserQuery())).ToGenericResult();
            })
                .WithName("GetBasketByUser")
                .MapToApiVersion(1, 0)
                .Produces<IEnumerable<BasketDto>>(StatusCodes.Status200OK);
            return group;
        }
    }
}