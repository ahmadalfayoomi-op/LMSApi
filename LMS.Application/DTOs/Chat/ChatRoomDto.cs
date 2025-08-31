

namespace LMS.Application.DTOs.Chat
{
    public class ChatRoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsGroup { get; set; }
        public List<ChatMessageDto> Messages { get; set; } = new();
        public List<int> ParticipantIds { get; set; } = new();
    }

}
