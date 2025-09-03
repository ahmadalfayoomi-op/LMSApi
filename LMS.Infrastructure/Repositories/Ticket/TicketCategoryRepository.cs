using AutoMapper;
using LMS.Application.DTOs.Ticket;
using LMS.Application.Interfaces.Ticket;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories.Ticket
{
    public class TicketCategoryRepository : ITicketCategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TicketCategoryRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TicketCategoryDto>> GetAllAsync( CancellationToken ct = default)
        {
            var categories = await _context.TicketCategories.Include(c => c.KnowledgeBaseArticles).ToListAsync(ct);
            return _mapper.Map<List<TicketCategoryDto>>(categories);
        }

        public async Task<TicketCategoryDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var category = await _context.TicketCategories.Include(c => c.KnowledgeBaseArticles)
                .FirstOrDefaultAsync(c => c.Id == id , ct);
            return _mapper.Map<TicketCategoryDto?>(category);
        }

        public async Task<TicketCategoryDto> AddAsync(TicketCategoryDto dto, CancellationToken ct = default)
        {
            var ticketCategory = _mapper.Map<TicketCategory>(dto);
            _context.TicketCategories.Add(ticketCategory);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<TicketCategoryDto>(ticketCategory);
        }

        public async Task<TicketCategoryDto> UpdateAsync(TicketCategoryDto dto, CancellationToken ct = default)
        {
            var ticketCategory = await _context.TicketCategories.FindAsync(new object[] { dto.Id }, ct);
            if (ticketCategory == null) throw new KeyNotFoundException("Ticket Category Base not found");

            _mapper.Map(dto, ticketCategory);
            _context.TicketCategories.Update(ticketCategory);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<TicketCategoryDto>(ticketCategory);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _context.TicketCategories.FindAsync(id);
            if (entity != null)
            {
                _context.TicketCategories.Remove(entity);
                await _context.SaveChangesAsync(ct);
            }
        }
    }
}
