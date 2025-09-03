using FluentValidation;
using LMS.Domain.Entities;


namespace LMS.Application.Validators.Course
{

    public class FavoriteCourseValidator : AbstractValidator<FavoriteCourse>
    {
        public FavoriteCourseValidator()
        {
            RuleFor(f => f.StudentId)
                .GreaterThan(0).WithMessage("StudentId must be a valid positive number.");

            RuleFor(f => f.CourseId)
                .GreaterThan(0).WithMessage("CourseId must be a valid positive number.");

            RuleFor(f => f.CreatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("CreatedAt cannot be in the future.");


        }
    }

}
