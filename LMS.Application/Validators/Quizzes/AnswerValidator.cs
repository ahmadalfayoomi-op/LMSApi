
using FluentValidation;
using LMS.Domain.Entities;

namespace LMS.Application.Validators.Quizzes
{
    public class AnswerValidator : AbstractValidator<Answer>
    {
        public AnswerValidator()
        {
            RuleFor(a => a.Text)
                .NotEmpty().WithMessage("Answer text is required.")
                .MaximumLength(500).WithMessage("Answer text must not exceed 500 characters.");

            RuleFor(a => a.QuestionId)
                .GreaterThan(0).WithMessage("QuestionId must be a valid positive number.");

            RuleFor(a => a.Question)
                .NotNull().WithMessage("Answer must be linked to a question.");
        }
    }
}
