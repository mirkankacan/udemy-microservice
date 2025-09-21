using UdemyMicroservice.Basket.Api.Features.Baskets.Delete;

namespace UdemyMicroservice.Basket.Api.Features.Baskets.DeleteItem
{
    public static class DeleteBasketItemEndpoint
    {
        public static RouteGroupBuilder DeleteBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/item/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                return (await mediator.Send(new DeleteBasketItemCommand(id))).ToGenericResult();
            })
                .WithName("DeleteBasketItem")
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound);
            return group;
        }
    }
}