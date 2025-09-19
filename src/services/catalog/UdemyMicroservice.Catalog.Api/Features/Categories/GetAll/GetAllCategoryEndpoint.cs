namespace UdemyMicroservice.Catalog.Api.Features.Categories.GetAll
{
    public static class GetAllCategoryEndpoint
    {
        public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                return (await mediator.Send(new GetAllCategoryQuery())).ToGenericResult();
            })
                .WithName("GetAllCategory")
                .MapToApiVersion(1, 1)
                .Produces<IEnumerable<CategoryDto>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound);
            return group;
        }
    }
}