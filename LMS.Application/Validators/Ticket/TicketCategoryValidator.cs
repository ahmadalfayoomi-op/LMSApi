using FluentValidation;
using LMS.Domain.Entities;

namespace LMS.Application.Validators.Ticket
{

    public class TicketCategoryValidator : AbstractValidator<TicketCategory>
    {
        public TicketCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(150).WithMessage("Name must not exceed 150 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.ParentCategoryId)
                .GreaterThan(0).When(x => x.ParentCategoryId.HasValue)
                .WithMessage("ParentCategoryId must be greater than 0 if provided.");
        }
    }

}
