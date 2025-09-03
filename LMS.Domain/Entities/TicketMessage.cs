
namespace LMS.Domain.Entities
{
    public class TicketMessage
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = default!;
        public int SenderId { get; set; }
        public User Sender { get; set; } = default!;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }

}
