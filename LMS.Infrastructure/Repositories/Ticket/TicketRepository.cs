using AutoMapper;
using LMS.Application.DTOs.Ticket;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Ticket;
using LMS.Domain.Entities;
using LMS.Domain.Enums;
using LMS.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;



namespace LMS.Infrastructure.Repositories.Ticket
{

    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IUserContextService _contextService;

        public TicketRepository(AppDbContext context, IFileService fileService, IMapper mapper, IUserContextService contextService)
        {
            _context = context;
            _fileService = fileService;
            _mapper = mapper;
            _contextService = contextService;
        }

        public async Task<TicketDto?> GetByIdAsync(int id, CancellationToken cd = default)
        {
            var ticket = await _context.Tickets
                .Include(t => t.User)
                .Include(t => t.Messages).ThenInclude(m => m.Sender)
                .FirstOrDefaultAsync(t => t.Id == id , cd);

            if (ticket == null) return null;

            return new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                Priority = ticket.Priority,
                CreatedAt = ticket.CreatedAt,
                CreatedByName = ticket.User.Username,
                Messages = ticket.Messages.Select(m => new TicketMessageDto
                {
                    Id = m.Id,
                    Message = m.Message,
                    CreatedAt = m.CreatedAt,
                    SenderName = m.Sender.Username
                }).ToList()
            };
        }

        public async Task<IEnumerable<TicketDto>> GetAllAsync(CancellationToken cd = default)
        {
            return await _context.Tickets
                .Include(t => t.User)
                .Include(t => t.Messages).ThenInclude(m => m.Sender)
                .Select(ticket => new TicketDto
                {
                    Id = ticket.Id,
                    Title = ticket.Title,
                    Description = ticket.Description,
                    Status = ticket.Status,
                    Priority = ticket.Priority,
                    CreatedAt = ticket.CreatedAt,
                    CreatedByName = ticket.User.Username,
                    Messages = ticket.Messages.Select(m => new TicketMessageDto
                    {
                        Id = m.Id,
                        Message = m.Message,
                        CreatedAt = m.CreatedAt,
                        SenderName = m.Sender.Username
                    }).ToList()
                })
                .ToListAsync(cd);
        }



        public async Task<int> AddAsync(TicketDto dto , CancellationToken cd = default)
        {
            var ticket = _mapper.Map<LMS.Domain.Entities.Ticket> (dto);
            ticket.UserId = _contextService.GetUserId();
            ticket.Status = TicketStatus.Open;
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync(cd);

            if (dto.Files != null && dto.Files.Any())
            {
                foreach (var file in dto.Files)
                {
                    var fileUrl = await _fileService.SaveFileAsync(file, "App_File/tickets");
                    var attachment = new TicketAttachment
                    {
                        TicketId = ticket.Id,
                        FileName = file.FileName,
                        FileUrl = fileUrl
                    };
                    _context.TicketAttachments.Add(attachment);
                }
                await _context.SaveChangesAsync();
            }

            if (dto.InitialMessages != null && dto.InitialMessages.Any())
            {
                foreach (var msgDto in dto.InitialMessages)
                {
                    var message = new TicketMessage
                    {
                        TicketId = ticket.Id,
                        SenderId = _contextService.GetUserId(),
                        Message = msgDto.Message,
                        CreatedAt=DateTime.UtcNow
                    };
                    _context.TicketMessages.Add(message);
                }
            }

            return ticket.Id;
        }

        public async Task UpdateAsync(int id, TicketDto dto, CancellationToken cd = default)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return;

            ticket.Status = dto.Status;
            ticket.Priority = dto.Priority;
            await _context.SaveChangesAsync(cd);
        }

        public async Task DeleteAsync(int id, CancellationToken cd = default)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync(cd);
            }
        }

        public async Task<IEnumerable<TicketMessageDto>> GetMessagesAsync(int ticketId, CancellationToken cd = default)
        {
            return await _context.TicketMessages
                .Where(m => m.TicketId == ticketId)
                .Include(m => m.Sender)
                .Select(m => new TicketMessageDto
                {
                    Id = m.Id,
                    Message = m.Message,
                    CreatedAt = m.CreatedAt,
                    SenderName = m.Sender.Username
                })
                .ToListAsync(cd);
        }

        public async Task<int> AddMessageAsync(TicketMessageDto dto, int senderId, CancellationToken cd = default)
        {
            var message = new TicketMessage
            {
                TicketId = dto.TicketId,
                SenderId = senderId,
                Message = dto.Message
            };

            _context.TicketMessages.Add(message);
            await _context.SaveChangesAsync(cd);
            return message.Id;
        }







        // ------------------- Attachments -------------------

        public async Task<int> AddAttachmentAsync(int ticketId, IFormFile file , CancellationToken cd =default)
        {
            var fileUrl = await _fileService.SaveFileAsync(file, "App_File/tickets");

            var attachment = new TicketAttachment
            {
                TicketId = ticketId,
                FileName = file.FileName,
                FileUrl = fileUrl
            };

            _context.TicketAttachments.Add(attachment);
            await _context.SaveChangesAsync(cd);
            return attachment.Id;
        }

        public async Task<IEnumerable<TicketAttachmentDto>> GetAttachmentsAsync(int ticketId, CancellationToken cd = default)
        {
            var attachments = await _context.TicketAttachments
                .Where(a => a.TicketId == ticketId)
                .ToListAsync(cd);

            return _mapper.Map<List<TicketAttachmentDto>>(attachments);
        }

        public async Task DeleteAttachmentAsync(int attachmentId, CancellationToken cd = default)
        {
            var attachment = await _context.TicketAttachments.FindAsync(attachmentId);
            if (attachment != null)
            {
                _context.TicketAttachments.Remove(attachment);
                await _context.SaveChangesAsync(cd);
            }
        }
    }

}
