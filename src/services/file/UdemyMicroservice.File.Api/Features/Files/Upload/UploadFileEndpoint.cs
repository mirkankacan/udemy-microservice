using MediatR;
using UdemyMicroservice.Shared.Extensions;

namespace UdemyMicroservice.File.Api.Features.Files.Upload
{
    public static class UploadFileEndpoint
    {
        public static RouteGroupBuilder UploadFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (IFormFile file, IMediator mediator) =>
            {
                return (await mediator.Send(new UploadFileCommand(file))).ToGenericResult();
            })
                .WithName("UploadFile")
                .MapToApiVersion(1, 0)
                .Produces<UploadFileCommandResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .DisableAntiforgery();
            return group;
        }
    }
}