using LMS.Application.DTOs.Chat;
using LMS.Application.Interfaces.Chat;
using Microsoft.AspNetCore.SignalR;

namespace LMS.Infrastructure.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        private readonly IChatRepository _repo;

        public ChatHub(IChatRepository repo)
        {
            _repo = repo;
        }

        public async Task JoinRoom(int chatRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId.ToString());
        }

        public async Task LeaveRoom(int chatRoomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId.ToString());
        }

        public async Task SendMessage(ChatMessageDto messageDto)
        {
            var message = await _repo.SendMessageAsync(messageDto);

            await Clients.Group(messageDto.ChatRoomId.ToString())
                .SendAsync("ReceiveMessage", message);
        }

        public async Task CreateRoom(ChatRoomDto chatRoomDto)
        {
            var room = await _repo.CreateChatRoomAsync(chatRoomDto);

            // Notify all users that a new room is created
            await Clients.All.SendAsync("RoomCreated", room);
        }

        public async Task GetRooms()
        {
            var rooms = await _repo.GetChatRoomsForUserAsync();
            await Clients.Caller.SendAsync("ReceiveRooms", rooms);
        }
        public async Task GetMessages(int chatRoomId)
        {
            var messages = await _repo.GetMessagesAsync(chatRoomId);
            await Clients.Caller.SendAsync("ReceiveMessages", messages);
        }
    }

}
