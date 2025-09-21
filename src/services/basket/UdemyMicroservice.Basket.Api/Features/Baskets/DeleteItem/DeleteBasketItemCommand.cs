namespace UdemyMicroservice.Basket.Api.Features.Baskets.Delete
{
    public record DeleteBasketItemCommand(Guid Id) : IRequestByServiceResult<Unit>;
}