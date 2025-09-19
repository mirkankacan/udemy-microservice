using Mapster;
using UdemyMicroservice.Catalog.Api.Data;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext appDbContext) : IRequestHandler<CreateCourseCommand, ServiceResult<CreateCourseCommandResponse>>
    {
        public async Task<ServiceResult<CreateCourseCommandResponse>> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
        {
            var hasCategory = await appDbContext.Categories
                .AsNoTracking()
                .AnyAsync(c => c.Id == command.CategoryId, cancellationToken);
            if (hasCategory is false)
            {
                return ServiceResult<CreateCourseCommandResponse>.Error("Category not found", $"Category with id '{command.CategoryId}' not found", HttpStatusCode.NotFound);
            }

            var hasCourse = await appDbContext.Courses
            .AsNoTracking()
            .AnyAsync(c => c.Name == command.Name, cancellationToken);

            if (hasCourse is true)
            {
                return ServiceResult<CreateCourseCommandResponse>.Error("Course name already exist", $"The course name '{command.Name}' already exist", HttpStatusCode.BadRequest);
            }

            var newCourse = command.Adapt<Course>();
            newCourse.Feature = new Feature()
            {
                Duration = 0,
                Rating = 0,
                EducatorFullName = string.Empty
            };
            await appDbContext.Courses.AddAsync(newCourse);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return ServiceResult<CreateCourseCommandResponse>.SuccessAsCreated(new CreateCourseCommandResponse(newCourse.Id), $"/api/courses/{newCourse.Id}");
        }
    }
}