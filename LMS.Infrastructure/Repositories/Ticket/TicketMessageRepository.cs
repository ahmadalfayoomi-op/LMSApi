using LMS.Application.DTOs.Ticket;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Ticket;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LMS.Infrastructure.Repositories.Ticket
{
    public class TicketMessageRepository : ITicketMessageRepository
    {
        private readonly AppDbContext _context;
        private readonly IUserContextService _userContext;

        public TicketMessageRepository(AppDbContext context, IUserContextService userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<IEnumerable<TicketMessageDto>> GetByTicketIdAsync(int ticketId, CancellationToken cd = default)
        {
            return await _context.TicketMessages
                .Include(m => m.Sender)
                .Where(m => m.TicketId == ticketId)
                .Select(m => new TicketMessageDto
                {
                    Id = m.Id,
                    Message = m.Message,
                    CreatedAt = m.CreatedAt,
                    SenderName = m.Sender.Username
                })
                .ToListAsync(cd);
        }

        public async Task<int> AddAsync(TicketMessageDto dto, CancellationToken cd = default)
        {
            var message = new TicketMessage
            {
                TicketId = dto.TicketId,
                SenderId = _userContext.GetUserId(),
                Message = dto.Message
            };

            _context.TicketMessages.Add(message);
            await _context.SaveChangesAsync(cd);
            return message.Id;
        }
    }
}
