using MediatR;
using Microsoft.Extensions.FileProviders;
using System.Net;
using UdemyMicroservice.Shared;
using UdemyMicroservice.Shared.Services;

namespace UdemyMicroservice.File.Api.Features.Files.Upload
{
    public class UploadFileCommandHandler(IFileProvider fileProvider, IIdentityService identityService) : IRequestHandler<UploadFileCommand, ServiceResult<UploadFileCommandResponse>>
    {
        public async Task<ServiceResult<UploadFileCommandResponse>> Handle(UploadFileCommand command, CancellationToken cancellationToken)
        {
            if (command.File.Length == 0)
            {
                return ServiceResult<UploadFileCommandResponse>.Error(
                    "File is empty",
                    "The uploaded file is empty",
                    HttpStatusCode.BadRequest
                );
            }

            var fileExtension = Path.GetExtension(command.File.FileName).ToLowerInvariant();

            var targetFolder = fileExtension switch
            {
                ".jpg" or ".png" or ".jpeg" or ".webp" => "pictures",
                ".pdf" or ".docx" or ".xlsx" => "files",
                _ => null
            };

            if (targetFolder is null)
            {
                return ServiceResult<UploadFileCommandResponse>.Error(
                    "Invalid file type",
                    $"The file type {fileExtension} is not supported",
                    HttpStatusCode.BadRequest);
            }

            var newFileName = $"{Guid.NewGuid()}{fileExtension}";
            var baseFolderPath = fileProvider.GetFileInfo(targetFolder).PhysicalPath!;

            Directory.CreateDirectory(baseFolderPath);

            var uploadPath = Path.Combine(baseFolderPath, newFileName);

            await using var stream = new FileStream(uploadPath, FileMode.Create);
            await command.File.CopyToAsync(stream, cancellationToken);

            return ServiceResult<UploadFileCommandResponse>.SuccessAsCreated(
                new UploadFileCommandResponse(newFileName, uploadPath, command.File.FileName), uploadPath
            );
        }
    }
}