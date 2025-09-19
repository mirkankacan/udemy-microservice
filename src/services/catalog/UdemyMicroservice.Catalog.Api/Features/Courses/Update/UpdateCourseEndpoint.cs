using UdemyMicroservice.Shared.Filters;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.Update
{
    public static class UpdateCourseEndpoint
    {
        public static RouteGroupBuilder UpdateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/{id:guid}", async (Guid id, UpdateCourseCommand command, IMediator mediator) =>
            {
                var cmd = command with { Id = id };
                return (await mediator.Send(cmd)).ToGenericResult();
            })
                .WithName("UpdateCourse")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>();
            return group;
        }
    }
}