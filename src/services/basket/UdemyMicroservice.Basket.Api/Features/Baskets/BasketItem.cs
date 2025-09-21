namespace UdemyMicroservice.Basket.Api.Features.Baskets
{
    public class BasketItem
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? ImageUrl { get; set; }

        public decimal Price { get; set; } = default!;
        public decimal? DiscountedPrice { get; set; }

        public BasketItem(Guid id, string name, string? imageUrl, decimal price, decimal? discountedPrice)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Price = price;
            DiscountedPrice = discountedPrice;
        }
    }
}