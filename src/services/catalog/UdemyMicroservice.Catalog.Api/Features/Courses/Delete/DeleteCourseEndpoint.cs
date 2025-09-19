namespace UdemyMicroservice.Catalog.Api.Features.Courses.Delete
{
    public static class DeleteCourseEndpoint
    {
        public static RouteGroupBuilder DeleteCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                return (await mediator.Send(new DeleteCourseCommand(id))).ToGenericResult();
            })
                .WithName("DeleteCourse")
                .MapToApiVersion(1, 1)
                .Produces<Unit>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound);
            return group;
        }
    }
}