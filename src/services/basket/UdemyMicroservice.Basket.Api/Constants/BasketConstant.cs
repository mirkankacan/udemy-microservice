using Microsoft.Extensions.Caching.Distributed;

namespace UdemyMicroservice.Basket.Api.Constants
{
    public static class BasketConstant
    {
        public const string BasketCacheKey = "udemyMicroserviceBasket:{0}";

        // Cache Durations
        public static readonly TimeSpan AbsoluteExpiration = TimeSpan.FromDays(30);

        public static readonly TimeSpan SlidingExpiration = TimeSpan.FromHours(24);

        // Cache Options - Hazır kullanıma uygun
        public static readonly DistributedCacheEntryOptions CacheOptions = new()
        {
            AbsoluteExpirationRelativeToNow = AbsoluteExpiration,
            SlidingExpiration = SlidingExpiration
        };
    }
}