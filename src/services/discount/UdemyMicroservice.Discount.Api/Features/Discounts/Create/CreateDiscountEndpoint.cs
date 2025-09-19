using MediatR;
using UdemyMicroservice.Shared.Filters;

namespace UdemyMicroservice.Discount.Api.Features.Discounts.Create
{
    public static class CreateDiscountEndpoint
    {
        public static RouteGroupBuilder CreateDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateDiscountCommand command, IMediator mediator) =>
            {
                return (await mediator.Send(command)).ToGenericResult();
            })
                .WithName("CreateDiscount")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .AddEndpointFilter<ValidationFilter<CreateDiscountCommand>>();
            return group;
        }
    }
}