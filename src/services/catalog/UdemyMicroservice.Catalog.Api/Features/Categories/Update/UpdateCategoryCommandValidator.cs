namespace UdemyMicroservice.Catalog.Api.Features.Categories.Update
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
              .Must(id => id != Guid.Empty)
              .NotEmpty().WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .Length(4, 25).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");
        }
    }
}