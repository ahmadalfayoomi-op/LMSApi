using FluentValidation;
using LMS.Domain.Entities;


namespace LMS.Application.Validators.Quizzes
{

    public class QuizValidator : AbstractValidator<Quiz>
    {
        public QuizValidator()
        {
            RuleFor(q => q.Title)
                .NotEmpty().WithMessage("Quiz title is required.")
                .MaximumLength(200).WithMessage("Quiz title must not exceed 200 characters.");

            RuleFor(q => q.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.")
                .When(q => !string.IsNullOrEmpty(q.Description));

            RuleFor(q => q.CourseId)
                .GreaterThan(0).WithMessage("CourseId must be a valid positive number.");

            RuleFor(q => q.Duration)
                .Must(d => !d.HasValue || d.Value.TotalMinutes > 0)
                .WithMessage("Duration must be greater than zero if specified.");


        }
    }

}
