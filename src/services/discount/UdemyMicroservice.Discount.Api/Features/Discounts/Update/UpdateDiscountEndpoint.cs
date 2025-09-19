using MediatR;
using UdemyMicroservice.Shared.Filters;

namespace UdemyMicroservice.Discount.Api.Features.Discounts.Update
{
    public static class UpdateDiscountEndpoint
    {
        public static RouteGroupBuilder UpdateDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/{id:guid}", async (Guid id, UpdateDiscountCommand command, IMediator mediator) =>
            {
                var cmd = command with { Id = id };
                return (await mediator.Send(cmd)).ToGenericResult();
            })
                .WithName("UpdateDiscount")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .AddEndpointFilter<ValidationFilter<UpdateDiscountCommand>>();
            return group;
        }
    }
}