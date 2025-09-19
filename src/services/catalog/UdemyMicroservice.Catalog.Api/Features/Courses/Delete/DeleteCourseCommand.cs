namespace UdemyMicroservice.Catalog.Api.Features.Courses.Delete
{
    public record DeleteCourseCommand(Guid Id) : IRequestByServiceResult<Unit>;
}