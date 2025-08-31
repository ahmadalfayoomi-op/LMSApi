using LMS.Application.DTOs.Chat;
using LMS.Application.Interfaces.Chat;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _repo;

        public ChatController(IChatRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("room/create")]
        public async Task<IActionResult> CreateRoom([FromBody] ChatRoomDto chatRoomDto, CancellationToken ct)
        {
            var room = await _repo.CreateChatRoomAsync(chatRoomDto, ct);
            return Ok(room);
        }

        [HttpPost("message/send")]
        public async Task<IActionResult> SendMessage([FromForm] ChatMessageDto messageDto, IFormFile? attachment, CancellationToken ct)
        {
            var message = await _repo.SendMessageAsync(messageDto, attachment, ct);
            return Ok(message);
        }

        [HttpGet("rooms/{userId}")]
        public async Task<IActionResult> GetRooms(CancellationToken ct)
        {
            var rooms = await _repo.GetChatRoomsForUserAsync(ct);
            return Ok(rooms);
        }

        [HttpGet("messages/{chatRoomId}")]
        public async Task<IActionResult> GetMessages(int chatRoomId, CancellationToken ct)
        {
            var messages = await _repo.GetMessagesAsync(chatRoomId, ct);
            return Ok(messages);
        }
    }

}
