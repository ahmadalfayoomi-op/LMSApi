using FluentValidation;
using LMS.Domain.Entities;

namespace LMS.Application.Validators.Course
{


    public class CourseEventValidator : AbstractValidator<CourseEvent>
    {
        public CourseEventValidator()
        {
            RuleFor(e => e.CourseId)
                .GreaterThan(0).WithMessage("CourseId must be a valid positive number.");

            RuleFor(e => e.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(e => e.StartTime)
                .GreaterThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Start time must be in the future or present.");

            RuleFor(e => e.EndTime)
                .GreaterThan(e => e.StartTime)
                .WithMessage("End time must be after start time.");

            RuleFor(e => e.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.")
                .When(e => !string.IsNullOrEmpty(e.Description));
        }
    }

}
