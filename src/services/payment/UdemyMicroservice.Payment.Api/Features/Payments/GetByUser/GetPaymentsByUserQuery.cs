using UdemyMicroservice.Payment.Api.Features.Payments.Dtos;
using UdemyMicroservice.Shared;

namespace UdemyMicroservice.Payment.Api.Features.Payments.GetByUser
{
    public record GetPaymentsByUserQuery : IRequestByServiceResult<List<PaymentDto>>;
}