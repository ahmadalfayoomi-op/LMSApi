using LMS.Application.DTOs.User;


namespace LMS.Application.Interfaces.User
{
    public interface IRoleRepository
    {
        Task<List<RoleDto>> GetAllAsync(CancellationToken ct = default);
        Task<RoleDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task<RoleDto> AddAsync(RoleDto role, CancellationToken ct = default);
        Task<RoleDto> UpdateAsync(RoleDto role, CancellationToken ct = default);
    }
}
