using LMS.Application.Common;

namespace LMS.Domain.Entities
{
    public class TicketCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? ParentCategoryId { get; set; }
        public TicketCategory? ParentCategory { get; set; }
        public ICollection<KnowledgeBaseArticle> KnowledgeBaseArticles { get; set; } = new List<KnowledgeBaseArticle>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }

}
