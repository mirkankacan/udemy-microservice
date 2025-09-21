using MediatR;
using Microsoft.Extensions.FileProviders;
using System.Net;
using UdemyMicroservice.Shared;

namespace UdemyMicroservice.File.Api.Features.Files.Delete
{
    public class DeleteFileCommandHandler(IFileProvider fileProvider)
        : IRequestHandler<DeleteFileCommand, ServiceResult<Unit>>
    {
        public Task<ServiceResult<Unit>> Handle(DeleteFileCommand command, CancellationToken cancellationToken)
        {
            var fileExtension = Path.GetExtension(command.FileName).ToLowerInvariant();

            // Uzantıya göre hedef klasörü seç
            var targetFolder = fileExtension switch
            {
                ".jpg" or ".png" or ".jpeg" or ".webp" => "pictures",
                ".pdf" or ".docx" or ".xlsx" => "files",
                _ => null
            };

            if (targetFolder is null)
            {
                return Task.FromResult(ServiceResult<Unit>.Error(
                    "Invalid file type",
                    $"The file type {fileExtension} is not supported",
                    HttpStatusCode.BadRequest));
            }

            var folderPath = Path.Combine(fileProvider.GetFileInfo(targetFolder).PhysicalPath!, command.UserId.ToString());
            var filePath = Path.Combine(folderPath, command.FileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return Task.FromResult(ServiceResult<Unit>.SuccessAsOk(Unit.Value));
        }
    }
}