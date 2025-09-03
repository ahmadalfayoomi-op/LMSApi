using FluentValidation;
using LMS.Domain.Entities;

namespace LMS.Application.Validators.Student
{


    public class StudentProgressValidator : AbstractValidator<StudentProgress>
    {
        public StudentProgressValidator()
        {
            RuleFor(sp => sp.StudentId)
                .GreaterThan(0).WithMessage("StudentId must be a valid positive number.");

            RuleFor(sp => sp.LessonId)
                .GreaterThan(0).WithMessage("LessonId must be a valid positive number.");

            RuleFor(sp => sp.Student)
                .NotNull()
                .When(sp => sp.StudentId > 0)
                .WithMessage("StudentProgress must be linked to a valid Student.");

            RuleFor(sp => sp.Lesson)
                .NotNull()
                .When(sp => sp.LessonId > 0)
                .WithMessage("StudentProgress must be linked to a valid Lesson.");

            RuleFor(sp => sp.CreatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("CreatedAt cannot be in the future.");
        }
    }

}
