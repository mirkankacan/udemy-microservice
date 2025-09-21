using System.Text.Json.Serialization;

namespace UdemyMicroservice.Basket.Api.Features.Baskets.Dtos
{
    public record BasketDto
    {
        public List<BasketItemDto> Items { get; set; }
        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }
        [JsonIgnore]
        public bool IsDiscountApplied => DiscountRate.HasValue && DiscountRate > 0 && !string.IsNullOrEmpty(Coupon);

        public decimal TotalPrice => Items.Sum(item => item.Price);
        public decimal? TotalDiscountedPrice => !IsDiscountApplied ? null : Items.Sum(x => x.DiscountedPrice);

        public BasketDto(List<BasketItemDto> items)
        {
            Items = items;
        }
        public BasketDto()
        {
        }
    }
}