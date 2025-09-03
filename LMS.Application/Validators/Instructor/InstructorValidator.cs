using FluentValidation;

namespace LMS.Application.Validators.Instructor
{

    public class InstructorValidator : AbstractValidator<LMS.Domain.Entities.Instructor>
    {
        public InstructorValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId is required.");

            RuleFor(x => x.Addres)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");

            RuleFor(x => x.Language)
                .NotEmpty().WithMessage("Language is required.")
                .MaximumLength(100).WithMessage("Language must not exceed 100 characters.");

            RuleFor(x => x.Biography)
                .NotEmpty().WithMessage("Biography is required.")
                .MaximumLength(1000).WithMessage("Biography must not exceed 1000 characters.");

            RuleFor(x => x.Headline)
                .NotEmpty().WithMessage("Headline is required.")
                .MaximumLength(150).WithMessage("Headline must not exceed 150 characters.");
        }
    }

}
