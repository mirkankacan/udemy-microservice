using MediatR;
using UdemyMicroservice.Discount.Api.Features.Discounts.Dtos;

namespace UdemyMicroservice.Catalog.Api.Features.Categories.GetAll
{
    public static class GetAllDiscountEndpoint
    {
        public static RouteGroupBuilder GetAllDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                return (await mediator.Send(new GetAllDiscountQuery())).ToGenericResult();
            })
                .WithName("GetAllCategory")
                .MapToApiVersion(1, 0)
                .Produces<IEnumerable<DiscountDto>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound);
            return group;
        }
    }
}