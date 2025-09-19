using UdemyMicroservice.Catalog.Api.Features.Courses.Dtos;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.GetAllByUserId
{
    public record GetAllCourseByUserIdQuery(Guid Id) : IRequestByServiceResult<IEnumerable<CourseDto>>;
}