using FluentValidation;
using LMS.Domain.Entities;


namespace LMS.Application.Validators.Student
{

    public class StudentAssignmentValidator : AbstractValidator<StudentAssignment>
    {
        public StudentAssignmentValidator()
        {
            RuleFor(sa => sa.AssignmentId)
                .GreaterThan(0).WithMessage("AssignmentId must be a valid positive number.");

            RuleFor(sa => sa.StudentId)
                .GreaterThan(0).WithMessage("StudentId must be a valid positive number.");

            RuleFor(sa => sa.Assignment)
                .NotNull().WithMessage("StudentAssignment must be linked to a valid Assignment.");

            RuleFor(sa => sa.Student)
                .NotNull().WithMessage("StudentAssignment must be linked to a valid Student.");

            RuleFor(sa => sa.SubmissionUrl)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("SubmissionUrl must be a valid URL if provided.");

            RuleFor(sa => sa.Grade)
                .InclusiveBetween(0, 100)
                .When(sa => sa.Grade.HasValue)
                .WithMessage("Grade must be between 0 and 100 if provided.");

            RuleFor(sa => sa.CreatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("CreatedAt cannot be in the future.");
        }
    }

}
