using MediatR;

namespace UdemyMicroservice.Discount.Api.Features.Discounts.Delete
{
    public static class DeleteDiscountEndpoint
    {
        public static RouteGroupBuilder DeleteDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                return (await mediator.Send(new DeleteDiscountCommand(id))).ToGenericResult();
            })
                .WithName("DeleteDiscount")
                .MapToApiVersion(1, 0)
                .Produces<Unit>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound);

            return group;
        }
    }
}