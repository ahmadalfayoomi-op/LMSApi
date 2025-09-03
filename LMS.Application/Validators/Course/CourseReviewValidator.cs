using FluentValidation;
using LMS.Domain.Entities;

namespace LMS.Application.Validators.Course
{

    public class CourseReviewValidator : AbstractValidator<CourseReview>
    {
        public CourseReviewValidator()
        {
            RuleFor(r => r.CourseId)
                .GreaterThan(0).WithMessage("CourseId must be a valid positive number.");

            RuleFor(r => r.StudentId)
                .GreaterThan(0).WithMessage("StudentId must be a valid positive number.");

            RuleFor(r => r.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage("Rating must be between 1 and 5.");

            RuleFor(r => r.Comment)
                .MaximumLength(1000)
                .WithMessage("Comment must not exceed 1000 characters.")
                .When(r => !string.IsNullOrEmpty(r.Comment));

            RuleFor(r => r.CreatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("CreatedAt cannot be in the future.");
        }
    }

}
