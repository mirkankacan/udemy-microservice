using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using UdemyMicroservice.Basket.Api.Constants;
using UdemyMicroservice.Basket.Api.Features.Baskets;
using UdemyMicroservice.Shared.Services;

namespace UdemyMicroservice.Basket.Api.Services
{
    public class BasketService(IDistributedCache cache, IIdentityService identityService) : IBasketService
    {
        private readonly Guid _userId = identityService.GetUserId;
        private readonly string _cacheKey = string.Format(BasketConstant.BasketCacheKey, identityService.GetUserId);

        public Features.Baskets.Basket CreateBasket()
        {
            var basket = new Features.Baskets.Basket
            {
                UserId = _userId,
                Items = new List<BasketItem>()
            };
            return basket;
        }

        public async Task DeleteBasketFromCacheAsync(CancellationToken cancellationToken)
        {
            await cache.RemoveAsync(_cacheKey, cancellationToken);
        }

        public async Task<Features.Baskets.Basket?> GetBasketFromCacheAsync(CancellationToken cancellationToken)
        {
            var basketJson = await cache.GetStringAsync(_cacheKey, cancellationToken);
            if (string.IsNullOrEmpty(basketJson))
            {
                return null;
            }
            return JsonSerializer.Deserialize<Features.Baskets.Basket>(basketJson)!;
        }

        public async Task SaveBasketToCacheAsync(Features.Baskets.Basket basket, CancellationToken cancellationToken)
        {
            var basketJson = JsonSerializer.Serialize(basket);
            await cache.SetStringAsync(_cacheKey, basketJson, BasketConstant.CacheOptions, cancellationToken);
        }

        public void ApplyExistingDiscountToBasket(Features.Baskets.Basket basket)
        {
            if (!basket.IsDiscountApplied) return;

            foreach (var item in basket.Items)
            {
                item.DiscountedPrice = item.Price * (decimal)(1 - basket.DiscountRate);
            }
        }

        public void RemoveDiscountFromBasket(Features.Baskets.Basket basket)
        {
            basket.DiscountRate = null;
            basket.Coupon = null;
            foreach (var item in basket.Items)
            {
                item.DiscountedPrice = null;
            }
        }

        public void ApplyNewDiscountToBasket(Features.Baskets.Basket basket, float rate, string coupon)
        {
            basket.DiscountRate = rate;
            basket.Coupon = coupon;
            foreach (var item in basket.Items)
            {
                item.DiscountedPrice = item.Price * (decimal)(1 - rate);
            }
        }
    }
}