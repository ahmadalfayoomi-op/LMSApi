using LMS.Application.DTOs.Auth;


namespace LMS.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<RegisterDto> RegisterAsync(RegisterDto user, CancellationToken ct = default);

    }
}
