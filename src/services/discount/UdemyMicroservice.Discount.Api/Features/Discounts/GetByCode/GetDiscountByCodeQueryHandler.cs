namespace UdemyMicroservice.Discount.Api.Features.Discounts.GetByCode
{
    public class GetDiscountByCodeQueryHandler(AppDbContext appDbContext) : IRequestHandler<GetDiscountByCodeQuery, ServiceResult<GetDiscountByCodeQueryResponse>>
    {
        public async Task<ServiceResult<GetDiscountByCodeQueryResponse>> Handle(GetDiscountByCodeQuery query, CancellationToken cancellationToken)
        {
            var discount = await appDbContext.Discounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code.ToLower() == query.Code.ToLower(), cancellationToken);
            if (discount is null)
            {
                return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount not found", $"The discount with code {query.Code} was not found", HttpStatusCode.NotFound);
            }
            if (discount.ExpiredAt <= DateTime.UtcNow)
            {
                return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount expired", $"The discount with code {query.Code} has expired", HttpStatusCode.NotFound);
            }
            return ServiceResult<GetDiscountByCodeQueryResponse>.SuccessAsOk(new GetDiscountByCodeQueryResponse(discount.Code, discount.Rate));
        }
    }
}