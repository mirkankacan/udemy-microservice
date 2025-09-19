using UdemyMicroservice.Catalog.Api.Features.Courses.Dtos;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.GetById
{
    public record GetCourseByIdQuery(Guid Id) : IRequestByServiceResult<CourseDto>;
}