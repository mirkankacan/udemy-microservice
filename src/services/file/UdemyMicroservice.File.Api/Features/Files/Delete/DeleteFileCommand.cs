using MediatR;
using UdemyMicroservice.Shared;

namespace UdemyMicroservice.File.Api.Features.Files.Delete
{
    public record DeleteFileCommand(Guid UserId, string FileName) : IRequestByServiceResult<Unit>;
}