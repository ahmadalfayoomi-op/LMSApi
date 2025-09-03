

namespace LMS.Application.DTOs.Ticket
{
    public class TicketAttachmentDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
    }
}
