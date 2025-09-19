using UdemyMicroservice.Shared.Filters;

namespace UdemyMicroservice.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
            {
                return (await mediator.Send(command)).ToGenericResult();
            })
                .WithName("CreateCategory")
                .MapToApiVersion(1, 1)
                .Produces<Guid>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>();
            return group;
        }
    }
}