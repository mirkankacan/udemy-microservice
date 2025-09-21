namespace UdemyMicroservice.Order.Domain.Entities
{
    // Anemic Model => Only properties, no behavior
    // Rich Model => Properties + Behavior
    public class OrderItem : BaseEntity<int>
    {
        public Guid ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public Guid OrderId { get; set; } = default!;
        public Order Order { get; set; } = default!;

        public void SetItem(Guid productId, string productName, decimal price)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                throw new ArgumentNullException(nameof(productName), "Product name cannot be empty.");
            }
            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Price must be positive.");
            }
            this.ProductId = productId;
            this.ProductName = productName;
            this.Price = price;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newPrice), "New price must be positive.");
            }
            Price = newPrice;
        }

        public void ApplyDiscount(float discountRate)
        {
            if (discountRate < 0 || discountRate > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(discountRate), "Discount rate must be between 0 and 1.");
            }
            Price = this.Price * (1 - (decimal)discountRate);
        }

        public bool IsSameProduct(OrderItem item)
        {
            return this.ProductId == item.ProductId;
        }
    }
}