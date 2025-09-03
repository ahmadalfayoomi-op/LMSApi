using FluentValidation;

namespace LMS.Application.Validators.Student
{
    public class StudentValidator : AbstractValidator<LMS.Domain.Entities.Student>
    {
        public StudentValidator()
        {
            RuleFor(s => s.UserId)
                .GreaterThan(0).WithMessage("UserId must be a valid positive number.");

            RuleFor(s => s.Language)
                .NotEmpty().WithMessage("Language is required.")
                .MaximumLength(50).WithMessage("Language must not exceed 50 characters.");

            RuleFor(s => s.Biography)
                .MaximumLength(1000).WithMessage("Biography must not exceed 1000 characters.")
                .When(s => !string.IsNullOrEmpty(s.Biography));

            RuleFor(s => s.Headline)
                .MaximumLength(200).WithMessage("Headline must not exceed 200 characters.")
                .When(s => !string.IsNullOrEmpty(s.Headline));

        }
    }
}
