using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UdemyMicroservice.Payment.Api.Data;
using UdemyMicroservice.Payment.Api.Features.Payments.Dtos;
using UdemyMicroservice.Shared;
using UdemyMicroservice.Shared.Services;

namespace UdemyMicroservice.Payment.Api.Features.Payments.GetByUser
{
    public class GetPaymentsByUserQueryHandler(AppDbContext appDbContext, IIdentityService identityService) : IRequestHandler<GetPaymentsByUserQuery, ServiceResult<List<PaymentDto>>>
    {
        public async Task<ServiceResult<List<PaymentDto>>> Handle(GetPaymentsByUserQuery query, CancellationToken cancellationToken)
        {
            var userId = identityService.GetUserId;
            var payments = await appDbContext.Payments.Where(x => x.UserId == userId).ToListAsync(cancellationToken);
            var mappedPayments = payments.Adapt<List<PaymentDto>>();

            return ServiceResult<List<PaymentDto>>.SuccessAsOk(mappedPayments);
        }
    }
}