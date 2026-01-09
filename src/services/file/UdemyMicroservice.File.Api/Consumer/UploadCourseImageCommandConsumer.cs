using MassTransit;
using Microsoft.Extensions.FileProviders;
using UdemyMicroservice.Bus.Commands;
using UdemyMicroservice.Bus.Events;

namespace UdemyMicroservice.File.Api.Consumer
{
    [EntityName("file-microservice.upload-course-image-command.queue")]
    public class UploadCourseImageCommandConsumer(IServiceProvider sp) : IConsumer<UploadCourseImageCommand>
    {
        public async Task Consume(ConsumeContext<UploadCourseImageCommand> context)
        {
            await using var scope = sp.CreateAsyncScope();
            var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProvider>();

            var fileExtension = Path.GetExtension(context.Message.FileName).ToLowerInvariant();
            var targetFolder = fileExtension switch
            {
                ".jpg" or ".png" or ".jpeg" or ".webp" => "pictures",
                ".pdf" or ".docx" or ".xlsx" => "files",
                _ => null
            };
            var newFileName = $"{Guid.NewGuid()}{fileExtension}";

            var baseFolderPath = fileProvider.GetFileInfo(targetFolder).PhysicalPath!;

            Directory.CreateDirectory(baseFolderPath);

            var uploadPath = Path.Combine(baseFolderPath, newFileName);
            await System.IO.File.WriteAllBytesAsync(uploadPath, context.Message.Image);

            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
            await publishEndpoint.Publish(new CoursePictureUploadedEvent(context.Message.CourseId, uploadPath));
        }
    }
}