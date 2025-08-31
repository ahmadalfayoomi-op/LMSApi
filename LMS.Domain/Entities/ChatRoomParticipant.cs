
namespace LMS.Domain.Entities
{
    public class ChatRoomParticipant 
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
