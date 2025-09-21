namespace UdemyMicroservice.Discount.Api.Features.Discounts.Delete
{
    public record DeleteDiscountCommand(Guid Id) : IRequestByServiceResult<Unit>;
}