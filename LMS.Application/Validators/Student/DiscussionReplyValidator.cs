using FluentValidation;
using LMS.Domain.Entities;


namespace LMS.Application.Validators.Student
{

    public class DiscussionReplyValidator : AbstractValidator<DiscussionReply>
    {
        public DiscussionReplyValidator()
        {
            RuleFor(r => r.DiscussionId)
                .GreaterThan(0).WithMessage("DiscussionId must be a valid positive number.");

            RuleFor(r => r.StudentId)
                .GreaterThan(0).WithMessage("StudentId must be a valid positive number.");

            RuleFor(r => r.Message)
                .NotEmpty().WithMessage("Message cannot be empty.")
                .MaximumLength(1000).WithMessage("Message must not exceed 1000 characters.");

            RuleFor(r => r.CreatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("CreatedAt cannot be in the future.");


        }
    }

}
