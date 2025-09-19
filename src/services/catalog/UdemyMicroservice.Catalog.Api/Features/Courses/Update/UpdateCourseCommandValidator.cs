namespace UdemyMicroservice.Catalog.Api.Features.Courses.Update
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .Length(4, 100).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

            RuleFor(x => x.Description)
                 .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                 .Length(4, 1000).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

            RuleFor(x => x.Price)
                 .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                 .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");

            RuleFor(x => x.CategoryId)
                 .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                 .Must(id => id != Guid.Empty);
        }
    }
}