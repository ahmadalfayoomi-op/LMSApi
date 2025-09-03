using LMS.Application.DTOs.Ticket;

namespace LMS.Application.Interfaces.Ticket
{
    public interface ITicketMessageRepository
    {
        Task<IEnumerable<TicketMessageDto>> GetByTicketIdAsync(int ticketId , CancellationToken cd =default);
        Task<int> AddAsync(TicketMessageDto dto,CancellationToken cd = default);
    }
}
