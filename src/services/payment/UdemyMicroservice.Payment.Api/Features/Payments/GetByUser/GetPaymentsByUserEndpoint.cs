using MediatR;
using UdemyMicroservice.Payment.Api.Features.Payments.Dtos;
using UdemyMicroservice.Shared.Extensions;

namespace UdemyMicroservice.Payment.Api.Features.Payments.GetByUser
{
    public static class GetPaymentsByUserEndpoint
    {
        public static RouteGroupBuilder GetPaymentsByUserGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user", async (IMediator mediator) =>
            {
                return (await mediator.Send(new GetPaymentsByUserQuery())).ToGenericResult();
            })
                .WithName("GetPaymentsByUser")
                .MapToApiVersion(1, 0)
                .Produces<List<PaymentDto>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError);
            return group;
        }
    }
}