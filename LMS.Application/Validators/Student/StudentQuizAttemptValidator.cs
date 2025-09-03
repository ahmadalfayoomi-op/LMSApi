using FluentValidation;
using LMS.Domain.Entities;

namespace LMS.Application.Validators.Student
{

    public class StudentQuizAttemptValidator : AbstractValidator<StudentQuizAttempt>
    {
        public StudentQuizAttemptValidator()
        {
            RuleFor(a => a.StudentId)
                .GreaterThan(0).WithMessage("StudentId must be a valid positive number.");

            RuleFor(a => a.Student)
                .NotNull().WithMessage("StudentQuizAttempt must be linked to a valid Student.");

            RuleFor(a => a.QuizId)
                .GreaterThan(0).WithMessage("QuizId must be a valid positive number.");

            RuleFor(a => a.Quiz)
                .NotNull().WithMessage("StudentQuizAttempt must be linked to a valid Quiz.");

            RuleFor(a => a.StartedAt)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("StartedAt cannot be in the future.");

            RuleFor(a => a.FinishedAt)
                .GreaterThanOrEqualTo(a => a.StartedAt)
                .When(a => a.FinishedAt.HasValue)
                .WithMessage("FinishedAt cannot be earlier than StartedAt.")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .When(a => a.FinishedAt.HasValue)
                .WithMessage("FinishedAt cannot be in the future.");

            RuleFor(a => a.Score)
                .InclusiveBetween(0, 100)
                .WithMessage("Score must be between 0 and 100.");

        }
    }

}
