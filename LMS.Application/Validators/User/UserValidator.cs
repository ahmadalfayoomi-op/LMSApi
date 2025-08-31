using FluentValidation;
using LMS.Application.DTOs.Auth;


namespace LMS.Application.Validators.User
{
    public class UserValidator : AbstractValidator<RegisterDto>
    {
        public UserValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}
