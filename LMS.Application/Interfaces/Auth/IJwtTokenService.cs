using LMS.Application.DTOs.User;


namespace LMS.Application.Interfaces.Auth
{
    public interface IJwtTokenService
    {
        string GenerateToken(UserDto user , int? studentId);
    }
}
