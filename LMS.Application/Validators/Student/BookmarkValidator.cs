using FluentValidation;
using LMS.Domain.Entities;


namespace LMS.Application.Validators.Student
{
    public class BookmarkValidator : AbstractValidator<Bookmark>
    {
        public BookmarkValidator()
        {
            RuleFor(b => b.StudentId)
                .GreaterThan(0).WithMessage("StudentId must be a valid positive number.");

            RuleFor(b => b.LessonId)
                .GreaterThan(0).WithMessage("LessonId must be a valid positive number.");

            RuleFor(b => b.CreatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CreatedAt cannot be in the future.");

        }
    }
}
