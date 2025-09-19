namespace UdemyMicroservice.Catalog.Api.Features.Categories.Create
{
    public class UpdateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .Length(4, max: 25).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");
        }
    }
}