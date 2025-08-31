using FluentValidation;
using LMS.Application.DTOs.Course;


namespace LMS.Application.Validators.Course
{
    public class EnrollmentValidator : AbstractValidator<EnrollmentDto>
    {
        public EnrollmentValidator()
        {
            RuleFor(c => c.CourseId).NotEmpty().WithMessage("Course is required");
        }
    }
}
