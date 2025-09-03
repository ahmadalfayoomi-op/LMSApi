using LMS.Domain.Common;

namespace LMS.Domain.Entities
{
    public class TicketAttachment : BaseEntity
    {
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = default!;
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
    }
}
