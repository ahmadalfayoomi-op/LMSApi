
namespace LMS.Domain.Entities
{
    public class ChatMessage 
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string Message { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string? AttachmentUrl { get; set; }
        public string? AttachmentType { get; set; }

        public bool IsRead { get; set; } = false;
    }
}
