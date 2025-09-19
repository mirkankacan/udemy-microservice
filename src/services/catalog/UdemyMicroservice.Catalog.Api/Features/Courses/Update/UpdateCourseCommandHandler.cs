using Mapster;
using UdemyMicroservice.Catalog.Api.Data;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.Update
{
    public class UpdateCourseCommandHandler(AppDbContext appDbContext) : IRequestHandler<UpdateCourseCommand, ServiceResult<UpdateCourseCommandResponse>>
    {
        public async Task<ServiceResult<UpdateCourseCommandResponse>> Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
        {
            var hasCourse = await appDbContext.Courses.AsNoTracking().AnyAsync(x => x.Id == command.Id, cancellationToken);
            if (hasCourse is false)
            {
                return ServiceResult<UpdateCourseCommandResponse>.Error("Course not found", $"Course with id '{command.Id}' not found", HttpStatusCode.NotFound);
            }

            var mappedCourse = command.Adapt<Course>();
            appDbContext.Courses.Update(mappedCourse);
            await appDbContext.SaveChangesAsync(cancellationToken);
            return ServiceResult<UpdateCourseCommandResponse>.SuccessAsOk(new UpdateCourseCommandResponse(mappedCourse.Id));
        }
    }
}