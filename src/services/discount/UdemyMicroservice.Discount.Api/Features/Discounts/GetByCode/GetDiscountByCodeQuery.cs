using UdemyMicroservice.Shared;

namespace UdemyMicroservice.Discount.Api.Features.Discounts.GetByCode
{
    public record GetDiscountByCodeQuery(string Code) : IRequestByServiceResult<GetDiscountByCodeQueryResponse>;
}