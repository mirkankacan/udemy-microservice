namespace UdemyMicroservice.Discount.Api.Features.Discounts
{
    public class DiscountEntity : BaseEntity
    {
        public Guid UserId { get; set; } = default!;
        public float Rate { get; set; } = default!;
        public string Code { get; set; } = default!;
        public DateTime ExpiredAt { get; set; } = default!;
    }
}