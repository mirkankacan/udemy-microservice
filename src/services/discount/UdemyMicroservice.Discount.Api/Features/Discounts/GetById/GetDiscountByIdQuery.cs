using UdemyMicroservice.Discount.Api.Features.Discounts.Dtos;
using UdemyMicroservice.Shared;

namespace UdemyMicroservice.Discount.Api.Features.Discounts.GetById
{
    public record GetDiscountByIdQuery(Guid Id) : IRequestByServiceResult<DiscountDto>;
}