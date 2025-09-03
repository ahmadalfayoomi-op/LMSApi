using LMS.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace LMS.Application.DTOs.Ticket
{
    public class TicketDto
    {
        public int Id { get; set; }            
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int? TicketCategoryId { get; set; }
        public string? TicketCategoryName { get; set; }

        public int UserId { get; set; }
        public string? CreatedByName { get; set; }

        public int? AssignedToId { get; set; }
        public string? AssignedToName { get; set; }

        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public List<TicketMessageDto>? Messages { get; set; }
        public List<TicketAttachmentDto>? Attachments { get; set; }

        public List<IFormFile>? Files { get; set; }
        public List<TicketMessageDto>? InitialMessages { get; set; }

    }

}
