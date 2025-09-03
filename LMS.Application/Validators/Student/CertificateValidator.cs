using FluentValidation;
using LMS.Domain.Entities;


namespace LMS.Application.Validators.Student
{
    public class CertificateValidator : AbstractValidator<Certificate>
    {
        public CertificateValidator()
        {
            RuleFor(c => c.StudentId)
                .GreaterThan(0).WithMessage("StudentId must be a valid positive number.");

            RuleFor(c => c.CourseId)
                .GreaterThan(0).WithMessage("CourseId must be a valid positive number.");

            RuleFor(c => c.IssuedAt)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("IssuedAt cannot be in the future.");

            RuleFor(c => c.CertificateUrl)
                .NotEmpty().WithMessage("CertificateUrl is required.")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("CertificateUrl must be a valid URL.");


        }
    }
}
