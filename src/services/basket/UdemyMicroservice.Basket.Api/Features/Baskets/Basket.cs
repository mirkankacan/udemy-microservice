using System.Text.Json.Serialization;

namespace UdemyMicroservice.Basket.Api.Features.Baskets
{
    public class Basket
    {
        public Guid UserId { get; set; }
        public List<BasketItem> Items { get; set; } = new();
        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }

        [JsonIgnore]
        public bool IsDiscountApplied => DiscountRate.HasValue && DiscountRate > 0 && !string.IsNullOrEmpty(Coupon);

        [JsonIgnore]
        public decimal TotalPrice => Items.Sum(item => item.Price);

        [JsonIgnore]
        public decimal? TotalDiscountedPrice => !IsDiscountApplied ? null : Items.Sum(x => x.DiscountedPrice);

        public Basket()
        {
        }

        public Basket(Guid userId, List<BasketItem> items)
        {
            UserId = userId;
            Items = items;
        }
    }
}