namespace UdemyMicroservice.Basket.Api.Features.Baskets.AddItem
{
    public record AddBasketItemCommand(Guid Id, string Name, decimal Price, string? ImageUrl) : IRequestByServiceResult<Unit>;
}