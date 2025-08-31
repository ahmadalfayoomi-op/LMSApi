using FluentValidation;
using LMS.Application.DTOs.User;


namespace LMS.Application.Validators.User
{
    public class RoleValidator : AbstractValidator<RoleDto>
    {
        public RoleValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Role name is required");
        }
    }
}
