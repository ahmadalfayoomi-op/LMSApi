using LMS.Application.DTOs.Chat;
using Microsoft.AspNetCore.Http;


namespace LMS.Application.Interfaces.Chat
{
    public interface IChatRepository
    {
        Task<ChatRoomDto> CreateChatRoomAsync(ChatRoomDto chatRoomDto, CancellationToken ct = default);
        Task<ChatMessageDto> SendMessageAsync(ChatMessageDto messageDto, IFormFile? attachment = null, CancellationToken ct = default);
        Task<IEnumerable<ChatRoomDto>> GetChatRoomsForUserAsync(CancellationToken ct = default);
        Task<IEnumerable<ChatMessageDto>> GetMessagesAsync(int chatRoomId, CancellationToken ct = default);
    }

}
