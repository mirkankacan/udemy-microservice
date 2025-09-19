using UdemyMicroservice.Discount.Api.Features.Discounts.Dtos;
using UdemyMicroservice.Shared;

namespace UdemyMicroservice.Discount.Api.Features.Discounts.GetById
{
    public record GetDiscountByCodeQuery(Guid Id) : IRequestByServiceResult<DiscountDto>;
}