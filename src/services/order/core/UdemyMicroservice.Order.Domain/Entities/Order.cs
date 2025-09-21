using MassTransit;
using UdemyMicroservice.Order.Domain.Enums;

namespace UdemyMicroservice.Order.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public string Code { get; set; } = default!;
        public Guid BuyerId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid? PaymentId { get; set; }
        public float? DiscountRate { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public int AddressId { get; set; }

        public Address Address { get; set; } = default!;

        public static Order CreateUnpaidOrder(Guid buyerId, float? discountRate, int addressId)
        {
            return new Order
            {
                Id = NewId.NextGuid(),
                Code = GenerateOrderCode(),
                BuyerId = buyerId,
                Status = OrderStatus.WaitingForPayment,
                TotalPrice = 0,
                OrderItems = new List<OrderItem>(),
                AddressId = addressId,
                DiscountRate = discountRate ?? 0
            };
        }

        public void AddOrderItem(Guid productId, string productName, decimal price)
        {
            var orderItem = new OrderItem();
            orderItem.SetItem(productId, productName, price);

            OrderItems.Add(orderItem);
            CalculateTotalPrice();
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

        private static string GenerateOrderCode()
        {
            var random = new Random();
            return $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{random.Next(1000, 9999)}";
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = OrderItems.Sum(item => item.Price);
            if (DiscountRate.HasValue)
            {
                TotalPrice = TotalPrice * (1 - (decimal)DiscountRate);
            }
        }
    }
}