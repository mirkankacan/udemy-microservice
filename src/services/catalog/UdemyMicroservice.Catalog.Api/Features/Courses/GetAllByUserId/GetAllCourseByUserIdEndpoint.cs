using UdemyMicroservice.Catalog.Api.Features.Courses.Dtos;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.GetAllByUserId
{
    public static class GetAllCourseByUserIdEndpoint
    {
        public static RouteGroupBuilder GetAllCourseByUserIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user/{userId:guid}", async (Guid userId, IMediator mediator) =>
            {
                return (await mediator.Send(new GetAllCourseByUserIdQuery(userId))).ToGenericResult();
            })
                .WithName("GetAllByUserId")
                .MapToApiVersion(1, 0)
                .Produces<IEnumerable<CourseDto>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound);
            return group;
        }
    }
}