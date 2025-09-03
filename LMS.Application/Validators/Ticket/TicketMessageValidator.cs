using FluentValidation;
using LMS.Domain.Entities;

namespace LMS.Application.Validators.Ticket
{
 

    public class TicketMessageValidator : AbstractValidator<TicketMessage>
    {
        public TicketMessageValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message cannot be empty.")
                .MaximumLength(2000).WithMessage("Message cannot exceed 2000 characters.");


        }
    }

}
