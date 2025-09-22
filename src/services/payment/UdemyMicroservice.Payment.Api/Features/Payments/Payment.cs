using MassTransit;

namespace UdemyMicroservice.Payment.Api.Features.Payments
{
    // Rich domain model
    public class Payment
    {
        public Guid Id { get; set; } = default!;
        public Guid UserId { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public string OrderCode { get; set; } = default!;
        public decimal Amount { get; set; } = default!;
        public PaymentStatus Status { get; set; }

        public Payment(Guid userId, string orderCode, decimal amount)
        {
            Create(userId, orderCode, amount);
        }

        public void Create(Guid userId, string orderCode, decimal amount)
        {
            Id = NewId.NextGuid();
            CreatedAt = DateTime.UtcNow;
            UserId = userId;
            OrderCode = orderCode;
            Amount = amount;
            Status = PaymentStatus.Pending;
        }

        public void SetStatus(PaymentStatus status)
        {
            Status = status;
        }
    }

    public enum PaymentStatus
    {
        Pending = 0,
        Success = 1,
        Failed = 2
    }
}