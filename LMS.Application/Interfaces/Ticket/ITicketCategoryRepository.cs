using LMS.Application.DTOs.Ticket;


namespace LMS.Application.Interfaces.Ticket
{
    public interface ITicketCategoryRepository
    {
        Task<IEnumerable<TicketCategoryDto>> GetAllAsync(CancellationToken ct = default);
        Task<TicketCategoryDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<TicketCategoryDto> AddAsync(TicketCategoryDto dto, CancellationToken ct = default);
        Task<TicketCategoryDto> UpdateAsync(TicketCategoryDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
