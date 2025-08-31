
using AutoMapper;
using LMS.Application.DTOs.Others;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Others;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories.Others
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;


        public NotificationRepository(AppDbContext context, IMapper mapper, IUserContextService userContext)
        {
            _context = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IEnumerable<NotificationDto>> GetByUserAsync(CancellationToken ct = default)
        {
            int userId = _userContext.GetUserId();
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }

        public async Task<NotificationDto> AddAsync(NotificationDto notificationDto, CancellationToken ct = default)
        {
            var entity = _mapper.Map<LMS.Domain.Entities.Notification>(notificationDto);
            _context.Notifications.Add(entity);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<NotificationDto>(entity);
        }

        public async Task MarkAsReadAsync(int notificationId, CancellationToken ct = default)
        {
            var notification = await _context.Notifications.FindAsync(new object[] { notificationId }, ct);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync(ct);
            }
        }
    }
}
