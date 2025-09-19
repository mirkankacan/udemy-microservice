using UdemyMicroservice.Shared;

namespace UdemyMicroservice.Discount.Api.Features.Discounts.Create
{
    public record CreateDiscountCommand(string Code, float Rate, Guid UserId, DateTime ExpiredAt) : IRequestByServiceResult<CreateDiscountCommandResponse>;
}