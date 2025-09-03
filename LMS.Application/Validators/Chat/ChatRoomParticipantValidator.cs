using FluentValidation;
using LMS.Domain.Entities;
namespace LMS.Application.Validators.Chat
{


    public class ChatRoomParticipantValidator : AbstractValidator<ChatRoomParticipant>
    {
        public ChatRoomParticipantValidator()
        {
            RuleFor(p => p.ChatRoomId)
                .GreaterThan(0).WithMessage("ChatRoomId must be a valid positive number.");

            RuleFor(p => p.UserId)
                .GreaterThan(0).WithMessage("UserId must be a valid positive number.");

            RuleFor(p => p.JoinedAt)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("JoinedAt cannot be in the future.");

            // Optional navigation validations (if entity is loaded with related data)
            RuleFor(p => p.ChatRoom)
                .NotNull().WithMessage("Participant must be linked to a ChatRoom.");

            RuleFor(p => p.User)
                .NotNull().WithMessage("Participant must be linked to a User.");
        }
    }

}
