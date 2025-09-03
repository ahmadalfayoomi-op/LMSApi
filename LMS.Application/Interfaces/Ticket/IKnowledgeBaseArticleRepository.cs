using LMS.Application.DTOs.Ticket;


namespace LMS.Application.Interfaces.Ticket
{
    public interface IKnowledgeBaseArticleRepository
    {
        Task<IEnumerable<KnowledgeBaseArticleDto>> GetAllAsync(CancellationToken ct = default);
        Task<KnowledgeBaseArticleDto?> GetByIdAsync(int id , CancellationToken ct = default);
        Task<KnowledgeBaseArticleDto> AddAsync(KnowledgeBaseArticleDto dto , CancellationToken ct = default);
        Task<KnowledgeBaseArticleDto> UpdateAsync(KnowledgeBaseArticleDto dto , CancellationToken ct = default);

        Task DeleteAsync(int id , CancellationToken ct = default);
        Task<IEnumerable<KnowledgeBaseArticleDto>> SearchAsync(string query, int? categoryId = null , CancellationToken ct = default);
    }
}
