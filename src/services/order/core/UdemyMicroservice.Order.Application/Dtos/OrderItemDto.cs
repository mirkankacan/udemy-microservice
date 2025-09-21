namespace UdemyMicroservice.Order.Application.Dtos
{
    public record OrderItemDto(Guid ProductId, string ProductName, decimal Price);
}