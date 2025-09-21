using UdemyMicroservice.Order.Application.Dtos;
using UdemyMicroservice.Shared;

namespace UdemyMicroservice.Order.Application.Features.Orders.GetByBuyer
{
    public record GetOrdersByBuyerIdQuery : IRequestByServiceResult<List<OrderDto>>;
}