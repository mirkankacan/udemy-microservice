using UdemyMicroservice.Catalog.Api.Features.Courses.Dtos;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.GetById
{
    public static class GetCourseByIdEndpoint
    {
        public static RouteGroupBuilder GetCourseByIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                return (await mediator.Send(new GetCourseByIdQuery(id))).ToGenericResult();
            })
                .WithName("GetCourseById")
                .MapToApiVersion(1, 0)
                .Produces<CourseDto>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound);
            return group;
        }
    }
}