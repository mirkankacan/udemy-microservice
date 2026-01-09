namespace UdemyMicroservice.Bus.Commands
{
    public record UploadCourseImageCommand(Guid CourseId, Byte[] Image, string FileName);
}