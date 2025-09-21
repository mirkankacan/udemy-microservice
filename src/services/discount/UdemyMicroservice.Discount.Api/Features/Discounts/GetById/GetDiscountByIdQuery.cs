namespace UdemyMicroservice.Discount.Api.Features.Discounts.GetById
{
    public record GetDiscountByCodeQuery(Guid Id) : IRequestByServiceResult<DiscountDto>;
}