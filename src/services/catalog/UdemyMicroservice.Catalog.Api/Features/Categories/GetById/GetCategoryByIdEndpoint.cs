namespace UdemyMicroservice.Catalog.Api.Features.Categories.GetById
{
    public static class GetCategoryByIdEndpoint
    {
        public static RouteGroupBuilder GetCategoryByIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                return (await mediator.Send(new GetCategoryByIdQuery(id))).ToGenericResult();
            }).WithName("GetCategoryById")
            .MapToApiVersion(1, 0)
              .Produces<CategoryDto>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status404NotFound);
            return group;
        }
    }
}