using MediatR;
using UdemyMicroservice.Order.Application.Dtos;
using UdemyMicroservice.Shared;

namespace UdemyMicroservice.Order.Application.Features.Orders.Create
{
    public record CreateOrderCommand(float? DiscountRate, List<OrderItemDto> Items, AddressDto Address, PaymentDto Payment) : IRequestByServiceResult<Unit>;
}