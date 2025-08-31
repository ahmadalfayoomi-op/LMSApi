using AutoMapper;
using LMS.Application.DTOs.Chat;
using LMS.Application.Interfaces.Chat;
using LMS.Application.Interfaces.Configuration;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace LMS.Infrastructure.Repositories.Chat
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IUserContextService _userContext;


        public ChatRepository(AppDbContext context, IMapper mapper, IFileService fileService, IUserContextService userContext)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
            _userContext = userContext;
        }

        public async Task<ChatRoomDto> CreateChatRoomAsync(ChatRoomDto chatRoomDto, CancellationToken ct = default)
        {
            var chatRoom = new ChatRoom
            {
                Name = chatRoomDto.Name,
                IsGroup = chatRoomDto.IsGroup
            };

            foreach (var UserId in chatRoomDto.ParticipantIds)
            {
                chatRoom.Participants.Add(new ChatRoomParticipant
                {
                    UserId = UserId
                });
            }

            _context.ChatRooms.Add(chatRoom);
            await _context.SaveChangesAsync(ct);

            return _mapper.Map<ChatRoomDto>(chatRoom);
        }

        public async Task<ChatMessageDto> SendMessageAsync(ChatMessageDto messageDto, IFormFile? attachment = null, CancellationToken ct = default)
        {
            var message = new ChatMessage
            {
                ChatRoomId = messageDto.ChatRoomId,
                UserId = _userContext.GetUserId(),
                Message = messageDto.Message,
                SentAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            if (attachment != null)
            {
                message.AttachmentUrl = await _fileService.SaveFileAsync(attachment, "App_File/chat");
                message.AttachmentType = attachment.ContentType;
            }

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync(ct);

            return _mapper.Map<ChatMessageDto>(message);
        }

        public async Task<IEnumerable<ChatRoomDto>> GetChatRoomsForUserAsync(CancellationToken ct = default)
        {
            int userId = _userContext.GetUserId();
            var rooms = await _context.ChatRooms
                .Include(r => r.Participants)
                .Include(r => r.Messages)
                .Where(r => r.Participants.Any(p => p.UserId == userId))
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<ChatRoomDto>>(rooms);
        }

        public async Task<IEnumerable<ChatMessageDto>> GetMessagesAsync(int chatRoomId, CancellationToken ct = default)
        {
            var messages = await _context.ChatMessages
                .Where(m => m.ChatRoomId == chatRoomId)
                .OrderBy(m => m.SentAt)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<ChatMessageDto>>(messages);
        }
    }

}
