
namespace LMS.Application.DTOs.Ticket
{
    public class TicketMessageDto
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int SenderId { get; set; }
        public string? SenderName { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
