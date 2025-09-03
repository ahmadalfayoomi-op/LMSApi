using FluentValidation;
using LMS.Domain.Entities;
namespace LMS.Application.Validators.Ticket
{


    public class TicketAttachmentValidator : AbstractValidator<TicketAttachment>
    {
        public TicketAttachmentValidator()
        {

            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("FileName is required.")
                .MaximumLength(255).WithMessage("FileName must not exceed 255 characters.");

            RuleFor(x => x.FileUrl)
                .NotEmpty().WithMessage("FileUrl is required.")
                .MaximumLength(500).WithMessage("FileUrl must not exceed 500 characters.")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute))
                .WithMessage("FileUrl must be a valid URL.");
        }
    }

}
