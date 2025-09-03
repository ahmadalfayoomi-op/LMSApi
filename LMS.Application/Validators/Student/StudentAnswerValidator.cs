

using FluentValidation;
using LMS.Domain.Entities;

namespace LMS.Application.Validators.Student
{
    public class StudentAnswerValidator : AbstractValidator<StudentAnswer>
    {
        public StudentAnswerValidator()
        {
            RuleFor(sa => sa.AttemptId)
                .GreaterThan(0).WithMessage("AttemptId must be a valid positive number.");

            RuleFor(sa => sa.QuestionId)
                .GreaterThan(0).WithMessage("QuestionId must be a valid positive number.");

            RuleFor(sa => sa.Attempt)
                .NotNull().WithMessage("StudentAnswer must be linked to a valid StudentQuizAttempt.");

            RuleFor(sa => sa.Question)
                .NotNull().WithMessage("StudentAnswer must be linked to a valid Question.");

            RuleFor(sa => sa)
                .Must(sa => sa.AnswerId.HasValue || !string.IsNullOrEmpty(sa.FreeTextAnswer))
                .WithMessage("Either AnswerId or FreeTextAnswer must be provided.");
        }
    }
}
