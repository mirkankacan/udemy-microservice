using UdemyMicroservice.Basket.Api.Services;

namespace UdemyMicroservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(IBasketService basketService) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult<bool>>
    {
        public async Task<ServiceResult<bool>> Handle(ApplyDiscountCouponCommand command, CancellationToken cancellationToken)
        {
            var basket = await basketService.GetBasketFromCacheAsync(cancellationToken);
            if (basket is null || !basket.Items.Any())
            {
                return ServiceResult<bool>.Error("Basket not found",
                    "The basket is empty or does not exist",
                    System.Net.HttpStatusCode.NotFound);
            }
            basketService.ApplyNewDiscountToBasket(basket, command.Rate, command.Coupon);
            await basketService.SaveBasketToCacheAsync(basket, cancellationToken);
            return ServiceResult<bool>.SuccessAsOk(true);
        }
    }
}