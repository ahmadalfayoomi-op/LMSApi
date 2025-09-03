using FluentValidation;
using LMS.Domain.Entities;

namespace LMS.Application.Validators.Others
{
 

    public class NotificationValidator : AbstractValidator<Notification>
    {
        public NotificationValidator()
        {
            RuleFor(n => n.UserId)
                .GreaterThan(0).WithMessage("UserId must be a valid positive number.");

            RuleFor(n => n.Title)
                .MaximumLength(200)
                .WithMessage("Title must not exceed 200 characters.")
                .When(n => !string.IsNullOrEmpty(n.Title));

            RuleFor(n => n.Message)
                .MaximumLength(1000)
                .WithMessage("Message must not exceed 1000 characters.")
                .When(n => !string.IsNullOrEmpty(n.Message));

            RuleFor(n => n.CreatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("CreatedAt cannot be in the future.");


        }
    }

}
