using LMS.Application.DTOs.Ticket;
using Microsoft.AspNetCore.Http;

namespace LMS.Application.Interfaces.Ticket
{

    public interface ITicketRepository
    {
        Task<TicketDto?> GetByIdAsync(int id , CancellationToken cd = default);
        Task<IEnumerable<TicketDto>> GetAllAsync(CancellationToken cd = default);
        Task<int> AddAsync(TicketDto dto,  CancellationToken cd = default);
        Task UpdateAsync(int id, TicketDto dto, CancellationToken cd = default);
        Task DeleteAsync(int id, CancellationToken cd = default);
        Task<IEnumerable<TicketMessageDto>> GetMessagesAsync(int ticketId, CancellationToken cd = default);
        Task<int> AddMessageAsync(TicketMessageDto dto, int senderId, CancellationToken cd = default);

        Task<int> AddAttachmentAsync(int ticketId, IFormFile file, CancellationToken cd = default);
        Task<IEnumerable<TicketAttachmentDto>> GetAttachmentsAsync(int ticketId, CancellationToken cd = default);
        Task DeleteAttachmentAsync(int attachmentId, CancellationToken cd = default);
    }
}
