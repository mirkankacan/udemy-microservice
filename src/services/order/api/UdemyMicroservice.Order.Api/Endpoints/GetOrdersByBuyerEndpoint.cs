using MediatR;
using UdemyMicroservice.Order.Application.Dtos;
using UdemyMicroservice.Order.Application.Features.Orders.GetByBuyer;
using UdemyMicroservice.Shared.Extensions;

namespace UdemyMicroservice.Order.Api.Endpoints
{
    public static class GetOrdersByBuyerEndpoint
    {
        public static RouteGroupBuilder GetOrdersByBuyerGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                return (await mediator.Send(new GetOrdersByBuyerIdQuery())).ToGenericResult();
            })
                .WithName("GetOrdersByBuyer")
                .MapToApiVersion(1, minorVersion: 0)
                .Produces<List<OrderDto>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound);
            return group;
        }
    }
}