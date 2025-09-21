using UdemyMicroservice.Shared;

namespace UdemyMicroservice.File.Api.Features.Files.Upload
{
    public record UploadFileCommand(IFormFile File) : IRequestByServiceResult<UploadFileCommandResponse>;
}