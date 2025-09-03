

using FluentValidation;
using LMS.Domain.Entities;

namespace LMS.Application.Validators.Quizzes
{
    public class QuestionValidator : AbstractValidator<Question>
    {
        public QuestionValidator()
        {
            RuleFor(q => q.Text)
                .NotEmpty().WithMessage("Question text is required.")
                .MaximumLength(1000).WithMessage("Question text must not exceed 1000 characters.");

            RuleFor(q => q.Type)
                .IsInEnum().WithMessage("Question type must be a valid enum value.");

            RuleFor(q => q.QuizId)
                .GreaterThan(0).WithMessage("QuizId must be a valid positive number.");


        }
    }
}
