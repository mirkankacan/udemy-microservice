using UdemyMicroservice.Catalog.Api.Features.Courses.Dtos;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.GetAll
{
    public static class GetAllCourseEndpoint
    {
        public static RouteGroupBuilder GetAllCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                return (await mediator.Send(new GetAllCourseQuery())).ToGenericResult();
            })
                .WithName("GetAllCourse")
                .MapToApiVersion(1, 0)
                .Produces<IEnumerable<CourseDto>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound);
            return group;
        }
    }
}