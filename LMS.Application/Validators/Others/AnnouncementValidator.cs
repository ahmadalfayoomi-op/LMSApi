using FluentValidation;
using LMS.Domain.Entities;

using System.Threading.Tasks;

namespace LMS.Application.Validators.Others
{
    public class AnnouncementValidator : AbstractValidator<Announcement>
    {
        public AnnouncementValidator()
        {
            RuleFor(a => a.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(a => a.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MinimumLength(10).WithMessage("Content must be at least 10 characters long.");

            RuleFor(a => a.PublishedAt)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("PublishedAt cannot be in the future.");
        }
    }
}
