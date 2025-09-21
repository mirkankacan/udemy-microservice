namespace UdemyMicroservice.Basket.Api.Features.Baskets.AddItem
{
    public class AddBasketItemCommandValidator : AbstractValidator<AddBasketItemCommand>
    {
        public AddBasketItemCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}