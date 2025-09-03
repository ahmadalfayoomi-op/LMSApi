
namespace LMS.Application.DTOs.Ticket
{
    public class TicketCategoryDto
    {
        public int Id { get; set; }  
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<KnowledgeBaseArticleDto>? Articles { get; set; }
    }
}
