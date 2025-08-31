using LMS.Application.DTOs.Others;


namespace LMS.Application.Interfaces.Others
{
    public interface IBadgeRepository
    {
        Task<IEnumerable<BadgeDto>> GetAllAsync(CancellationToken ct = default);
        Task<BadgeDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<BadgeDto> AddAsync(BadgeDto badgeDto, CancellationToken ct = default);
        Task<BadgeDto> UpdateAsync(BadgeDto badgeDto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }

}
