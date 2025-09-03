using FluentValidation;
using LMS.Domain.Entities;


namespace LMS.Application.Validators.Course
{
    public class LessonValidator : AbstractValidator<Lesson>
    {
        public LessonValidator()
        {
            RuleFor(l => l.Title)
                .NotEmpty().WithMessage("Lesson title is required.")
                .MaximumLength(200).WithMessage("Lesson title must not exceed 200 characters.");

            RuleFor(l => l.ContentUrl)
                .NotEmpty().WithMessage("ContentUrl is required.")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("ContentUrl must be a valid URL.");

            RuleFor(l => l.CourseId)
                .GreaterThan(0).WithMessage("CourseId must be a valid positive number.");


        }
    }
}
