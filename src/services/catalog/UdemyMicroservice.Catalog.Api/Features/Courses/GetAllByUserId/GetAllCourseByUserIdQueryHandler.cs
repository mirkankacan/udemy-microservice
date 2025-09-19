using Mapster;
using UdemyMicroservice.Catalog.Api.Features.Courses.Dtos;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.GetAllByUserId
{
    public class GetAllCourseByUserIdQueryHandler(AppDbContext appDbContext) : IRequestHandler<GetAllCourseByUserIdQuery, ServiceResult<IEnumerable<CourseDto>>>
    {
        public async Task<ServiceResult<IEnumerable<CourseDto>>> Handle(GetAllCourseByUserIdQuery query, CancellationToken cancellationToken)
        {
            var courses = await appDbContext.Courses.AsNoTracking().Where(x => x.UserId == query.Id).ToListAsync(cancellationToken);
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