using UdemyMicroservice.Catalog.Api.Features.Courses.Dtos;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.GetAll
{
    public record GetAllCourseQuery : IRequestByServiceResult<IEnumerable<CourseDto>>;
}