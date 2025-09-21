using FluentValidation;

namespace UdemyMicroservice.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Order must contain at least one item.")
                .Must(items => items != null && items.All(item => item.Price > 0)).WithMessage("All items must have a valid price greater than zero.");

            RuleFor(x => x.Address.Province)
                 .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                 .Length(3, max: 100).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

            RuleFor(x => x.Address.District)
                    .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                    .Length(3, max: 100).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

            RuleFor(x => x.Address.Street)
                  .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                  .Length(3, max: 200).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

            RuleFor(x => x.Address.ZipCode)
                  .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                  .Length(3, max: 20).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

            RuleFor(x => x.Address.Line)
                  .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                  .Length(3, max: 300).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");
        }
    }
}