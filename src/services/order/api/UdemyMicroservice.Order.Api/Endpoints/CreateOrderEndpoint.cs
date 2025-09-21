using MediatR;
using Microsoft.AspNetCore.Mvc;
using UdemyMicroservice.Order.Application.Features.Orders.Create;
using UdemyMicroservice.Shared.Extensions;
using UdemyMicroservice.Shared.Filters;

namespace UdemyMicroservice.Order.Api.Endpoints
{
    public static class CreateOrderEndpoint
    {
        public static RouteGroupBuilder CreateOrderGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromBody] CreateOrderCommand command, IMediator mediator) =>
            {
                return (await mediator.Send(command)).ToGenericResult();
            })
                .WithName("CreateOrder")
                .MapToApiVersion(1, 0)
                .Produces<Unit>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .AddEndpointFilter<ValidationFilter<CreateOrderCommand>>();
            return group;
        }
    }
}