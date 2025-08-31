using FluentValidation;
using LMS.Application.DTOs.Course;


namespace LMS.Application.Validators.Course
{
    public class CourseValidator : AbstractValidator<CourseDto>
    {
        public CourseValidator()
        {
            RuleFor(c => c.Title).NotEmpty().WithMessage("Course title is required");
            RuleFor(c => c.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(c => c.CategoryId).NotEmpty().WithMessage("Category is required");
        }
    }
}
