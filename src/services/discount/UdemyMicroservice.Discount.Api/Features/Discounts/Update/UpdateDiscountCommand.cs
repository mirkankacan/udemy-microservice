namespace UdemyMicroservice.Discount.Api.Features.Discounts.Update
{
    public record UpdateDiscountCommand(Guid Id, string Code, float Rate, Guid UserId, DateTime ExpiredAt) : IRequestByServiceResult<UpdateDiscountCommandResponse>;
}