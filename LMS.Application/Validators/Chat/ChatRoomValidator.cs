
using FluentValidation;
using LMS.Domain.Entities;

namespace LMS.Application.Validators.Chat
{
    public class ChatRoomValidator : AbstractValidator<ChatRoom>
    {
        public ChatRoomValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Chat room name is required.")
                .MaximumLength(200).WithMessage("Chat room name must not exceed 200 characters.");

            RuleFor(r => r.IsGroup)
                .NotNull().WithMessage("IsGroup flag must be specified.");

            RuleFor(r => r.Participants)
                .Must(p => p != null && p.Any())
                .WithMessage("Chat room must have at least one participant.")
                .When(r => !r.IsGroup);

            RuleFor(r => r.Participants)
                .Must(p => p != null && p.Count >= 2)
                .WithMessage("Group chat must have at least two participants.")
                .When(r => r.IsGroup);
        }
    }
}
