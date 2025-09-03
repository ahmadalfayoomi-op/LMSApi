using AutoMapper;
using LMS.Application.DTOs.Others;
using LMS.Application.DTOs.Ticket;
using LMS.Application.Interfaces.Ticket;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LMS.Infrastructure.Repositories.Ticket
{
    public class KnowledgeBaseArticleRepository : IKnowledgeBaseArticleRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public KnowledgeBaseArticleRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KnowledgeBaseArticleDto>> GetAllAsync(CancellationToken ct = default)
        {
            var articles = await _context.KnowledgeBaseArticles.Include(a => a.TicketCategory).ToListAsync(ct);
            return _mapper.Map<List<KnowledgeBaseArticleDto>>(articles);
        }

        public async Task<KnowledgeBaseArticleDto?> GetByIdAsync(int id , CancellationToken ct = default)
        {
            var article = await _context.KnowledgeBaseArticles.Include(a => a.TicketCategory)
                .FirstOrDefaultAsync(a => a.Id == id , ct);
            return _mapper.Map<KnowledgeBaseArticleDto?>(article);
        }
        public async Task<KnowledgeBaseArticleDto> AddAsync(KnowledgeBaseArticleDto dto, CancellationToken ct = default)
        {
            var knowledgeBase = _mapper.Map<KnowledgeBaseArticle>(dto);
            _context.KnowledgeBaseArticles.Add(knowledgeBase);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<KnowledgeBaseArticleDto>(knowledgeBase);
        }

        public async Task<KnowledgeBaseArticleDto> UpdateAsync(KnowledgeBaseArticleDto dto, CancellationToken ct = default)
        {
            var knowledgeBase = await _context.KnowledgeBaseArticles.FindAsync(new object[] { dto.Id }, ct);
            if (knowledgeBase == null) throw new KeyNotFoundException("knowledge Base not found");

            _mapper.Map(dto, knowledgeBase);
            _context.KnowledgeBaseArticles.Update(knowledgeBase);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<KnowledgeBaseArticleDto>(knowledgeBase);
        }


        public async Task DeleteAsync(int id , CancellationToken ct = default)
        {
            var entity = await _context.KnowledgeBaseArticles.FindAsync(id);
            if (entity != null)
            {
                _context.KnowledgeBaseArticles.Remove(entity);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task<IEnumerable<KnowledgeBaseArticleDto>> SearchAsync(string query, int? categoryId = null, CancellationToken ct = default)
        {
            var articles = _context.KnowledgeBaseArticles.Include(a => a.TicketCategory).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query))
                articles = articles.Where(a => a.Title.Contains(query) || a.Content.Contains(query));
            if (categoryId.HasValue)
                articles = articles.Where(a => a.CategoryId == categoryId);

            var result = await articles.ToListAsync(ct);
            return _mapper.Map<List<KnowledgeBaseArticleDto>>(result);
        }
    }
}
