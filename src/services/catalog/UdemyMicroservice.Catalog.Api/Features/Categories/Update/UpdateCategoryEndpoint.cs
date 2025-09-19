using UdemyMicroservice.Shared.Filters;

namespace UdemyMicroservice.Catalog.Api.Features.Categories.Update
{
    public static class UpdateCategoryEndpoint
    {
        public static RouteGroupBuilder UpdateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/{id:guid}", async (Guid id, UpdateCategoryCommand command, IMediator mediator) =>
            {
                var cmd = command with { Id = id };
                return (await mediator.Send(cmd)).ToGenericResult();
            })
                .WithName("UpdateCategory")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .AddEndpointFilter<ValidationFilter<UpdateCategoryCommand>>();
            return group;
        }
    }
}