namespace UdemyMicroservice.Payment.Api.Features.Payments.Dtos
{
    public record PaymentDto(Guid Id, string OrderCode, string Amount, DateTime CreatedAt, PaymentStatus Status);
}