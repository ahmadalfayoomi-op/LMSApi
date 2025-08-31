using LMS.Application.DTOs.Others;


namespace LMS.Application.Interfaces.Others
{
    public interface INotificationRepository
    {
        Task<IEnumerable<NotificationDto>> GetByUserAsync(CancellationToken ct = default);
        Task<NotificationDto> AddAsync(NotificationDto notificationDto, CancellationToken ct = default);
        Task MarkAsReadAsync(int notificationId, CancellationToken ct = default);
    }
}
