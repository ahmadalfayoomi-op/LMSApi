
namespace LMS.Application.DTOs.Chat
{
    public class ChatMessageDto
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public string? AttachmentUrl { get; set; }
        public string? AttachmentType { get; set; }
        public bool IsRead { get; set; }
    }
}
