using Mapster;
using MediatR;
using System.Net;
using UdemyMicroservice.Discount.Api.Features.Discounts.Dtos;
using UdemyMicroservice.Shared;

namespace UdemyMicroservice.Discount.Api.Features.Discounts.GetById
{
    public class GetDiscountByCodeQueryHandler(AppDbContext appDbContext) : IRequestHandler<GetDiscountByCodeQuery, ServiceResult<DiscountDto>>
    {
        public async Task<ServiceResult<DiscountDto>> Handle(GetDiscountByCodeQuery query, CancellationToken cancellationToken)
        {
            var discount = await appDbContext.Discounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
            if (discount is null)
            {
                return ServiceResult<DiscountDto>.Error("Discount not found", $"The discount with ID {query.Id} was not found", HttpStatusCode.NotFound);
            }
            var mappedDiscount = discount.Adapt<DiscountDto>();
            return ServiceResult<DiscountDto>.SuccessAsOk(mappedDiscount);
        }
    }
}