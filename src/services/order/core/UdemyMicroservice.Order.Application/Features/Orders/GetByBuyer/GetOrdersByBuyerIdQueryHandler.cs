using Mapster;
using MediatR;
using System.Net;
using UdemyMicroservice.Order.Application.Contracts.Repositories;
using UdemyMicroservice.Order.Application.Dtos;
using UdemyMicroservice.Shared;
using UdemyMicroservice.Shared.Services;

namespace UdemyMicroservice.Order.Application.Features.Orders.GetByBuyer
{
    public class GetOrdersByBuyerIdQueryHandler(IIdentityService identityService, IOrderRepository orderRepository) : IRequestHandler<GetOrdersByBuyerIdQuery, ServiceResult<List<OrderDto>>>
    {
        public async Task<ServiceResult<List<OrderDto>>> Handle(GetOrdersByBuyerIdQuery query, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrderByBuyerId(identityService.GetUserId, cancellationToken);
            if (orders is null)
            {
                return ServiceResult<List<OrderDto>>.Error("Orders not found for this user", HttpStatusCode.NotFound);
            }
            var mappedOrders = orders.Adapt<List<OrderDto>>();

            return ServiceResult<List<OrderDto>>.SuccessAsOk(mappedOrders);
        }
    }
}