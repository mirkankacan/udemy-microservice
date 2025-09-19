namespace UdemyMicroservice.Discount.Api.Features.Discounts.Dtos
{
    public record DiscountDto(Guid Id, string Code, float Rate, Guid UserId, DateTime ExpiredAt, DateTime CreatedAt, Guid CreatedBy, DateTime? UpdatedAt, Guid? UpdatedBy);
}