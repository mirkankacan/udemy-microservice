using Mapster;
using MediatR;
using System.Net;
using UdemyMicroservice.Discount.Api.Features.Discounts.Dtos;
using UdemyMicroservice.Shared;

namespace UdemyMicroservice.Catalog.Api.Features.Categories.GetAll
{
    public class GetAllDiscountQueryHandler(AppDbContext appDbContext) : IRequestHandler<GetAllDiscountQuery, ServiceResult<IEnumerable<DiscountDto>>>
    {
        public async Task<ServiceResult<IEnumerable<DiscountDto>>> Handle(GetAllDiscountQuery query, CancellationToken cancellationToken)
        {
            var discounts = await appDbContext.Discounts.AsNoTracking().ToListAsync(cancellationToken);
            if (discounts is null)
            {
                return ServiceResult<IEnumerable<DiscountDto>>.Error("No discounts found", "There are no discounts available", HttpStatusCode.NotFound);
            }

            var mappedDiscounts = discounts.Adapt<IEnumerable<DiscountDto>>();

            return ServiceResult<IEnumerable<DiscountDto>>.SuccessAsOk(mappedDiscounts);
        }
    }
}