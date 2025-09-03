using FluentValidation;

namespace LMS.Application.Validators.Assignment
{
    public class AssignmentValidator : AbstractValidator<LMS.Domain.Entities.Assignment>
    {
        public AssignmentValidator()
        {
            RuleFor(a => a.CourseId)
                .GreaterThan(0).WithMessage("CourseId must be a valid positive number.");

            RuleFor(a => a.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(a => a.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(10).WithMessage("Description must be at least 10 characters long.");

            RuleFor(a => a.DueDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.");
        }
    }
}
