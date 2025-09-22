using UdemyMicroservice.Shared;

namespace UdemyMicroservice.Payment.Api.Features.Payments.Create
{
    public record CreatePaymentCommand(string OrderCode, string CardNumber, string CardHolderName, string CardExpirationDate, string CardCvv, decimal Amount) : IRequestByServiceResult<Guid>;
}