

using FluentValidation;
using LMS.Domain.Entities;

namespace LMS.Application.Validators.Course
{
    public class CourseViewLogValidator : AbstractValidator<CourseViewLog>
    {
        public CourseViewLogValidator()
        {
            RuleFor(v => v.StudentId)
                .GreaterThan(0).WithMessage("StudentId must be a valid positive number.");

            RuleFor(v => v.CourseId)
                .GreaterThan(0)
                .When(v => v.CourseId.HasValue)
                .WithMessage("CourseId must be a valid positive number if provided.");

            RuleFor(v => v.LessonId)
                .GreaterThan(0)
                .When(v => v.LessonId.HasValue)
                .WithMessage("LessonId must be a valid positive number if provided.");

            RuleFor(v => v.ViewedAt)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("ViewedAt cannot be in the future.");


        }
    }
}
