namespace UdemyMicroservice.Bus.Events
{
    public record CoursePictureUploadedEvent(Guid CourseId, string ImageUrl);
}