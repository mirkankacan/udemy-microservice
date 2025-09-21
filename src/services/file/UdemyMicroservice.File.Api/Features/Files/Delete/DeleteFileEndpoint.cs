using MediatR;
using Microsoft.AspNetCore.Mvc;
using UdemyMicroservice.Shared.Extensions;

namespace UdemyMicroservice.File.Api.Features.Files.Delete
{
    public static class DeleteFileEndpoint
    {
        public static RouteGroupBuilder DeleteFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/", async ([FromBody] DeleteFileCommand command, [FromServices] IMediator mediator) =>
            {
                return (await mediator.Send(command)).ToGenericResult();
            })
                .WithName("DeleteFile")
                .MapToApiVersion(1, 0)
                .Produces<Unit>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest);
            return group;
        }
    }
}