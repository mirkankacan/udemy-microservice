namespace UdemyMicroservice.Catalog.Api.Features.Courses.Delete
{
    public class DeleteCourseCommandHandler(AppDbContext appDbContext) : IRequestHandler<DeleteCourseCommand, ServiceResult<Unit>>
    {
        public async Task<ServiceResult<Unit>> Handle(DeleteCourseCommand command, CancellationToken cancellationToken)
        {
            var course = await appDbContext.Courses.FindAsync(command.Id, cancellationToken);
            if (course is null)
            {
                return ServiceResult<Unit>.Error("Course not found", $"Course with id '{command.Id}' not found", HttpStatusCode.NotFound);
            }
            appDbContext.Courses.Remove(course);
            await appDbContext.SaveChangesAsync(cancellationToken);
            return ServiceResult<Unit>.SuccessAsOk(Unit.Value);
        }
    }
}