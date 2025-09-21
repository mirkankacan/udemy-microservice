namespace UdemyMicroservice.Order.Application.Dtos
{
    public record OrderDto(DateTime OrderDate, decimal TotalPrice, List<OrderItemDto> Items, AddressDto Address);
}