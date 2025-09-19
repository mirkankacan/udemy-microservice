using Mapster;
using UdemyMicroservice.Catalog.Api.Features.Courses.Dtos;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.GetById
{
    public class GetCourseByIdQueryHandler(AppDbContext appDbContext) : IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDto>>
    {
        public async Task<ServiceResult<CourseDto>> Handle(GetCourseByIdQuery query, CancellationToken cancellationToken)
        {
            var course = await appDbContext.Courses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
            if (course is null)
            {
                return ServiceResult<CourseDto>.Error("Course not found", $"Course with id '{query.Id}' not found", HttpStatusCode.NotFound);
            }
            var category = await appDbContext.Categories.AsNoTracking().FirstAsync(x => x.Id == course.CategoryId, cancellationToken);
            course.Category = category;
            var mappedCourse = course.Adapt<CourseDto>();
            return ServiceResult<CourseDto>.SuccessAsOk(mappedCourse);
        }
    }
}