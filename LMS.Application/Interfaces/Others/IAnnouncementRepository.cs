using LMS.Application.DTOs.Others;


namespace LMS.Application.Interfaces.Others
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<AnnouncementDto>> GetAllByCourseAsync(int courseId, CancellationToken ct = default);
        Task<AnnouncementDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<AnnouncementDto> AddAsync(AnnouncementDto announcementDto, CancellationToken ct = default);
        Task<AnnouncementDto> UpdateAsync(AnnouncementDto announcementDto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }

}
