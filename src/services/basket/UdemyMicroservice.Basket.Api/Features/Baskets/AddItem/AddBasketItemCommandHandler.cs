using UdemyMicroservice.Basket.Api.Services;

namespace UdemyMicroservice.Basket.Api.Features.Baskets.AddItem
{
    public class AddBasketItemCommandHandler(IBasketService basketService) : IRequestHandler<AddBasketItemCommand, ServiceResult<Unit>>
    {
        public async Task<ServiceResult<Unit>> Handle(AddBasketItemCommand command, CancellationToken cancellationToken)
        {
            var basket = await basketService.GetBasketFromCacheAsync(cancellationToken);
            if (basket is null)
            {
                basket = basketService.CreateBasket();
            }
            basket.Items.RemoveAll(x => x.Id == command.Id);

            basket.Items.Add(new BasketItem(
                command.Id,
                command.Name,
                command.ImageUrl,
                command.Price,
                null
            ));
            basketService.ApplyExistingDiscountToBasket(basket);
            await basketService.SaveBasketToCacheAsync(basket, cancellationToken);

            return ServiceResult<Unit>.SuccessAsCreated(Unit.Value, null);
        }
    }
}