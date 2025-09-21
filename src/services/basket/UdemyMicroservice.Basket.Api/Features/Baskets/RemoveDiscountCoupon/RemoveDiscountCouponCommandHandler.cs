using UdemyMicroservice.Basket.Api.Services;

namespace UdemyMicroservice.Basket.Api.Features.Baskets.RemoveDiscountCoupon
{
    public class RemoveDiscountCouponCommandHandler(IBasketService basketService) : IRequestHandler<RemoveDiscountCouponCommand, ServiceResult<bool>>
    {
        public async Task<ServiceResult<bool>> Handle(RemoveDiscountCouponCommand command, CancellationToken cancellationToken)
        {
            var basket = await basketService.GetBasketFromCacheAsync(cancellationToken);
            if (basket is null || !basket.Items.Any())
            {
                return ServiceResult<bool>.Error("Basket not found or empty", System.Net.HttpStatusCode.NotFound);
            }
            basketService.RemoveDiscountFromBasket(basket);
            await basketService.SaveBasketToCacheAsync(basket, cancellationToken);
            return ServiceResult<bool>.SuccessAsOk(true);
        }
    }
}