using MediatR;
using Microsoft.AspNetCore.Mvc;
using UdemyMicroservice.Shared.Extensions;
using UdemyMicroservice.Shared.Filters;

namespace UdemyMicroservice.Payment.Api.Features.Payments.Create
{
    public static class CreatePaymentEndpoint
    {
        public static RouteGroupBuilder CreatePaymentGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromBody] CreatePaymentCommand command, IMediator mediator) =>
            {
                return (await mediator.Send(command)).ToGenericResult();
            })
                .WithName("CreatePayment")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .AddEndpointFilter<ValidationFilter<CreatePaymentCommand>>().RequireAuthorization("ClientCredentials");
            return group;
        }
    }
}