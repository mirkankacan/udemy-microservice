namespace UdemyMicroservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponValidator : AbstractValidator<ApplyDiscountCouponCommand>
    {
        public ApplyDiscountCouponValidator()
        {
            RuleFor(x => x.Coupon)
               .NotEmpty().WithMessage("{PropertyName}  must not be empty.")
               .Length(3, max: 10).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

            RuleFor(x => x.Rate)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .LessThanOrEqualTo(1).WithMessage("{PropertyName} must be less than or equal to 1.");
        }
    }
}