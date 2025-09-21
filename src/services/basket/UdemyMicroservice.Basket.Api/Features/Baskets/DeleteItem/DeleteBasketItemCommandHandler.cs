using UdemyMicroservice.Basket.Api.Services;

namespace UdemyMicroservice.Basket.Api.Features.Baskets.Delete
{
    public class DeleteBasketItemCommandHandler(IBasketService basketService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult<Unit>>
    {
        public async Task<ServiceResult<Unit>> Handle(DeleteBasketItemCommand command, CancellationToken cancellationToken)
        {
            var basket = await basketService.GetBasketFromCacheAsync(cancellationToken);
            if (basket is null || !basket.Items.Any())
            {
                return ServiceResult<Unit>.Error("Basket not found",
                    "The basket is empty or does not exist",
                    System.Net.HttpStatusCode.NotFound);
            }
            var basketItemToDelete = basket.Items.FirstOrDefault(x => x.Id == command.Id);
            if (basketItemToDelete is null)
            {
                return ServiceResult<Unit>.Error("Item not found",
                    $"The item with ID {command.Id} was not found in the basket",
                    System.Net.HttpStatusCode.NotFound);
            }

            basket.Items.Remove(basketItemToDelete);

            if (!basket.Items.Any())
            {
                await basketService.DeleteBasketFromCacheAsync(cancellationToken);
            }
            else
            {
                await basketService.SaveBasketToCacheAsync(basket, cancellationToken);
            }
            return ServiceResult<Unit>.SuccessAsOk(Unit.Value);
        }
    }
}