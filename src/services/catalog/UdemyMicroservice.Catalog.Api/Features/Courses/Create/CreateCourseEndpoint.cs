using UdemyMicroservice.Shared.Filters;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.Create
{
    public static class CreateCourseEndpoint
    {
        public static RouteGroupBuilder CreateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCourseCommand command, IMediator mediator) =>
            {
                return (await mediator.Send(command)).ToGenericResult();
            })
                .WithName("CreateCourse")
                .MapToApiVersion(1, 1)
                .Produces<Guid>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>();
            return group;
        }
    }
}