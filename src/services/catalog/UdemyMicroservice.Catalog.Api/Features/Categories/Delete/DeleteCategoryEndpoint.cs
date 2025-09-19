namespace UdemyMicroservice.Catalog.Api.Features.Categories.Delete
{
    public static class DeleteCategoryEndpoint
    {
        public static RouteGroupBuilder DeleteCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                return (await mediator.Send(new DeleteCategoryCommand(id))).ToGenericResult();
            })
                .WithName("DeleteCategory")
                .MapToApiVersion(1, 0)
                .Produces<Unit>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound);

            return group;
        }
    }
}