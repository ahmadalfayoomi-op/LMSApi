using FluentValidation;

namespace LMS.Application.Validators.Ticket
{

    public class TicketValidator : AbstractValidator<LMS.Domain.Entities.Ticket>
    {
        public TicketValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Status must be a valid enum value.");

            RuleFor(x => x.Priority)
                .IsInEnum().WithMessage("Priority must be a valid enum value.");

        }
    }

}
