using FluentValidation;
using LMS.Domain.Entities;

namespace LMS.Application.Validators.Chat
{

    public class ChatMessageValidator : AbstractValidator<ChatMessage>
    {
        public ChatMessageValidator()
        {
            RuleFor(m => m.ChatRoomId)
                .GreaterThan(0).WithMessage("ChatRoomId must be a valid positive number.");

            RuleFor(m => m.UserId)
                .GreaterThan(0).WithMessage("UserId must be a valid positive number.");

            RuleFor(m => m.Message)
                .NotEmpty().WithMessage("Message cannot be empty.")
                .MaximumLength(1000).WithMessage("Message must not exceed 1000 characters.");

            RuleFor(m => m.SentAt)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("SentAt cannot be in the future.");

            RuleFor(m => m.UpdatedAt)
                .GreaterThanOrEqualTo(m => m.SentAt)
                .WithMessage("UpdatedAt cannot be earlier than SentAt.")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("UpdatedAt cannot be in the future.");

            RuleFor(m => m.AttachmentUrl)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("AttachmentUrl must be a valid URL if provided.");

            RuleFor(m => m.AttachmentType)
                .Must(type => string.IsNullOrEmpty(type) ||
                              new[] { "image", "video", "audio", "file" }.Contains(type.ToLower()))
                .WithMessage("AttachmentType must be one of: image, video, audio, file.");
        }
    }

}
