using UdemyMicroservice.Shared.Filters;

namespace UdemyMicroservice.Basket.Api.Features.Baskets.AddItem
{
    public static class AddBasketItemEndpoint
    {
        public static RouteGroupBuilder AddBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/item", async (AddBasketItemCommand command, IMediator mediator) =>
            {
                return (await mediator.Send(command)).ToGenericResult();
            })
                .WithName("AddBasketItem")
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status201Created)
                .AddEndpointFilter<ValidationFilter<AddBasketItemCommand>>();
            return group;
        }
    }
}