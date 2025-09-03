using LMS.Domain.Common;


namespace LMS.Domain.Entities
{
    public class ChatRoom : BaseEntity
    {
        public string Name { get; set; } = string.Empty; 
        public bool IsGroup { get; set; } = false;

        // Participants
        public ICollection<ChatRoomParticipant> Participants { get; set; } = new List<ChatRoomParticipant>();

        // Messages
        public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }
}
