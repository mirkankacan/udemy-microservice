namespace UdemyMicroservice.File.Api.Features.Files.Upload
{
    public record UploadFileCommandResponse(string FileName, string FilePath, string OriginalFileName);
}