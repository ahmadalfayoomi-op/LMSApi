using LMS.Application.DTOs.User;

namespace LMS.Application.Interfaces.User
{
    public interface IUserRepository
    {
        Task<UserDto?> GetByUsernameAsync(string username, CancellationToken ct = default);
        Task<UserDto> CreateAsync(UserDto user , CancellationToken ct = default);
        Task<List<UserDto>> GetAllAsync(CancellationToken ct = default);

    }

}
