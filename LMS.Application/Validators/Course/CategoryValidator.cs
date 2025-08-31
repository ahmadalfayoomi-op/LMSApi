using FluentValidation;
using LMS.Application.DTOs.Course;


namespace LMS.Application.Validators.Course
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Course title is required");
        }
    }
}
