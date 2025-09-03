using FluentValidation;
using LMS.Domain.Entities;

namespace LMS.Application.Validators.Student
{

    public class StudentNoteValidator : AbstractValidator<StudentNote>
    {
        public StudentNoteValidator()
        {
            RuleFor(n => n.StudentId)
                .GreaterThan(0).WithMessage("StudentId must be a valid positive number.");

            RuleFor(n => n.Student)
                .NotNull().WithMessage("StudentNote must be linked to a valid Student.");

            RuleFor(n => n.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(n => n.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(2000).WithMessage("Content must not exceed 2000 characters.");

            RuleFor(n => n.CreatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("CreatedAt cannot be in the future.");

            RuleFor(n => n.UpdatedAt)
                .GreaterThanOrEqualTo(n => n.CreatedAt)
                .WithMessage("UpdatedAt cannot be earlier than CreatedAt.")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("UpdatedAt cannot be in the future.");
        }
    }

}
