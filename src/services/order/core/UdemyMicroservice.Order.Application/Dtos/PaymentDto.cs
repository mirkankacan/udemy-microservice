namespace UdemyMicroservice.Order.Application.Dtos
{
    public record PaymentDto(string CardNumber, string CardHolderName, string Expiration, string Cvv, decimal Amount);
}