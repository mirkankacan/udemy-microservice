using Mapster;
using UdemyMicroservice.Catalog.Api.Features.Courses.Dtos;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.GetAll
{
    public class GetAllCourseQueryHandler(AppDbContext appDbContext) : IRequestHandler<GetAllCourseQuery, ServiceResult<IEnumerable<CourseDto>>>
    {
        public async Task<ServiceResult<IEnumerable<CourseDto>>> Handle(GetAllCourseQuery query, CancellationToken cancellationToken)
        {
            var courses = await appDbContext.Courses.AsNoTracking().ToListAsync(cancellationToken);
            if (courses is null)
            {
                return ServiceResult<IEnumerable<CourseDto>>.Error("No courses found", "There are no courses available", HttpStatusCode.NotFound);
            }
            var categories = await appDbContext.Categories.AsNoTracking().ToListAsync(cancellationToken);
            foreach (var course in courses)
            {
                course.Category = categories.First(x => x.Id == course.CategoryId);
            }
            var mappedCourses = courses.Adapt<IEnumerable<CourseDto>>();

            return ServiceResult<IEnumerable<CourseDto>>.SuccessAsOk(mappedCourses);
        }
    }
}