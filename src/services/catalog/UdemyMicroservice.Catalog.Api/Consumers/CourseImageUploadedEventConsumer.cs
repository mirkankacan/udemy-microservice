using UdemyMicroservice.Bus.Events;

namespace UdemyMicroservice.Catalog.Api.Consumers
{
    [EntityName("catalog-microservice.course-image-uploaded-event.queue")]
    public class CourseImageUploadedEventConsumer(IServiceProvider sp) : IConsumer<CoursePictureUploadedEvent>
    {
        public async Task Consume(ConsumeContext<CoursePictureUploadedEvent> context)
        {
            await using var scope = sp.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var course = dbContext.Courses.Find(context.Message.CourseId);
            if (course == null)
                throw new ArgumentNullException();

            course.ImageUrl = context.Message.ImageUrl;
            await dbContext.SaveChangesAsync();
        }
    }
}