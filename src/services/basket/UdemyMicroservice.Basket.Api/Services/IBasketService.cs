namespace UdemyMicroservice.Basket.Api.Services
{
    public interface IBasketService
    {
        Features.Baskets.Basket CreateBasket();

        Task DeleteBasketFromCacheAsync(CancellationToken cancellationToken);

        Task SaveBasketToCacheAsync(Features.Baskets.Basket basket, CancellationToken cancellationToken);

        Task<Features.Baskets.Basket> GetBasketFromCacheAsync(CancellationToken cancellationToken);

        void ApplyExistingDiscountToBasket(Features.Baskets.Basket basket);

        void RemoveDiscountFromBasket(Features.Baskets.Basket basket);

        void ApplyNewDiscountToBasket(Features.Baskets.Basket basket, float rate, string coupon);
    }
}