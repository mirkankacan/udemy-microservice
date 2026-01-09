using UdemyMicroservice.Order.Domain.Enums;

namespace UdemyMicroservice.Order.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public string Code { get; set; } = default!;
        public Guid BuyerId { get; set; } = default!;
        public OrderStatus Status { get; set; }
        public decimal LastPrice { get; set; }
        public Guid? PaymentId { get; set; }
        public float? DiscountRate { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public int AddressId { get; set; }

        public Address Address { get; set; } = default!;

        public static Order CreateUnpaidOrder(Guid buyerId, float? discountRate, int addressId)
        {
            return new Order
            {
                Id = Guid.CreateVersion7(),
                Code = GenerateOrderCode(),
                BuyerId = buyerId,
                Status = OrderStatus.WaitingForPayment,
                LastPrice = 0,
                OrderItems = new List<OrderItem>(),
                AddressId = addressId,
                DiscountRate = discountRate ?? 0
            };
        }

        public static Order CreateUnpaidOrder(Guid buyerId, float? discountRate)
        {
            return new Order
            {
                Id = Guid.CreateVersion7(),
                Code = GenerateOrderCode(),
                BuyerId = buyerId,
                Status = OrderStatus.WaitingForPayment,
                LastPrice = 0,
                OrderItems = new List<OrderItem>(),
                DiscountRate = discountRate ?? 0
            };
        }

        public void AddOrderItem(Guid productId, string productName, decimal price)
        {
            var orderItem = new OrderItem();
            if (DiscountRate.HasValue)
            {
                price = price * (1 - (decimal)DiscountRate.Value);
            }
            orderItem.SetItem(productId, productName, price);
            OrderItems.Add(orderItem);
            CalculateLastPrice();
        }

        public void SetPaidStatus(Guid paymentId)
        {
            if (Status != OrderStatus.WaitingForPayment)
            {
                throw new InvalidOperationException("Order status must be 'WaitingForPayment' to set it to 'Completed'.");
            }
            Status = OrderStatus.Completed;
            PaymentId = paymentId;
        }

        public void ApplyDiscount(float discountRate)
        {
            if (discountRate < 0 || discountRate > 1)
            {
                throw new ArgumentException("Invalid discount rate");
            }
            DiscountRate = discountRate;
            CalculateLastPrice();
        }

        private static string GenerateOrderCode()
        {
            var random = new Random();
            return $"{DateTime.UtcNow:MMddHHmm}{random.Next(10, 99)}";
        }

        private void CalculateLastPrice()
        {
            LastPrice = OrderItems.Sum(item => item.Price);
        }
    }
}