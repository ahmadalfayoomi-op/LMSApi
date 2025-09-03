using FluentValidation;
using LMS.Domain.Entities;


namespace LMS.Application.Validators.Student
{

    public class StudentBadgeValidator : AbstractValidator<StudentBadge>
    {
        public StudentBadgeValidator()
        {
            RuleFor(sb => sb.StudentId)
                .GreaterThan(0).WithMessage("StudentId must be a valid positive number.");

            RuleFor(sb => sb.BadgeId)
                .GreaterThan(0).WithMessage("BadgeId must be a valid positive number.");

            RuleFor(sb => sb.Student)
                .NotNull().WithMessage("StudentBadge must be linked to a valid Student.");

            RuleFor(sb => sb.Badge)
                .NotNull().WithMessage("StudentBadge must be linked to a valid Badge.");

            RuleFor(sb => sb.AwardedAt)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("AwardedAt cannot be in the future.");
        }
    }

}
