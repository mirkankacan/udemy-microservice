using Asp.Versioning.Builder;
using UdemyMicroservice.File.Api.Features.Files.Delete;
using UdemyMicroservice.File.Api.Features.Files.Upload;

namespace UdemyMicroservice.Discount.Api.Features.Discounts
{
    public static class FileEndpointExtension
    {
        public static void AddFileGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/files")
                .WithApiVersionSet(apiVersionSet)
                .WithTags("Files")
                .WithDescription("File management endpoints for uploading and managing files")
                .UploadFileGroupItemEndpoint()
                .DeleteFileGroupItemEndpoint()
                /*.RequireAuthorization()*/;
        }
    }
}