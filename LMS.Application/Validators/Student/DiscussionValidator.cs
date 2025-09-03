using FluentValidation;
using LMS.Domain.Entities;


namespace LMS.Application.Validators.Student
{
    public class DiscussionValidator : AbstractValidator<Discussion>
    {
        public DiscussionValidator()
        {
            RuleFor(d => d.LessonId)
                .GreaterThan(0).WithMessage("LessonId must be a valid positive number.");

            RuleFor(d => d.StudentId)
                .GreaterThan(0).WithMessage("StudentId must be a valid positive number.");

            RuleFor(d => d.Message)
                .NotEmpty().WithMessage("Message cannot be empty.")
                .MaximumLength(1000).WithMessage("Message must not exceed 1000 characters.");


        }
    }
}
