using LMS.Domain.Common;
using LMS.Domain.Enums;


namespace LMS.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public int UserId { get; set; }
        public User? User { get; set; } = default!;
        public int? AssignedToId { get; set; }
        public User? AssignedTo { get; set; }
        public int? TicketCategoryId { get; set; }
        public TicketCategory? TicketCategory { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public TicketPriority Priority { get; set; } = TicketPriority.Medium;
        public DateTime? AcknowledgedAt { get; set; }
        public DateTime? SlaDueAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public ICollection<TicketMessage> Messages { get; set; } = new List<TicketMessage>();
        public ICollection<TicketAttachment> Attachments { get; set; } = new List<TicketAttachment>();
    }

}
