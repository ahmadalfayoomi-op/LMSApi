using LMS.Domain.Common;

namespace LMS.Domain.Entities
{
    public class KnowledgeBaseArticle : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public TicketCategory? TicketCategory { get; set; }
    }
}
