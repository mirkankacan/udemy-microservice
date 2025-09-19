using FluentValidation;

namespace UdemyMicroservice.Discount.Api.Features.Discounts.Create
{
    public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .Length(3, max: 10).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

            RuleFor(x => x.Rate)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}")
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.UserId)
              .Must(userId => userId != Guid.Empty)
              .NotEmpty().WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.ExpiredAt)
              .NotEmpty().WithMessage("{PropertyName} cannot be empty")
              .GreaterThan(DateTime.UtcNow).WithMessage("{PropertyName} must be a future date (UTC)")
              .LessThan(DateTime.UtcNow.AddMonths(3)).WithMessage("Expiration date cannot be longer than 3 months from today (UTC)");
        }
    }
}