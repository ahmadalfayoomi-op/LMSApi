

using FluentValidation;
using LMS.Domain.Entities;

namespace LMS.Application.Validators.Others
{
    public class BadgeValidator : AbstractValidator<Badge>
    {
        public BadgeValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("Badge name is required.")
                .MaximumLength(100).WithMessage("Badge name must not exceed 100 characters.");

            RuleFor(b => b.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(b => b.IconUrl)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("IconUrl must be a valid URL if provided.");
        }
    }
}
