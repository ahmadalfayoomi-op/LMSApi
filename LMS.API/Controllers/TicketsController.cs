using LMS.Application.DTOs.Ticket;
using LMS.Application.Interfaces.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepo;
        private readonly ITicketMessageRepository _messageRepo;

        public TicketsController(ITicketRepository ticketRepo, ITicketMessageRepository messageRepo)
        {
            _ticketRepo = ticketRepo;
            _messageRepo = messageRepo;
        }


        [HttpGet]
        [Authorize(Policy = "Tickets.View")]

        public async Task<IActionResult> GetAll()
        {
            var tickets = await _ticketRepo.GetAllAsync();
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Tickets.View")]

        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _ticketRepo.GetByIdAsync(id);
            return ticket == null ? NotFound() : Ok(ticket);
        }

        [HttpPost]
        [Authorize(Policy = "Tickets.Create")]

        public async Task<IActionResult> Create([FromBody] TicketDto dto)
        {
            var ticketId = await _ticketRepo.AddAsync(dto);
            return Ok(ticketId);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Tickets.Update")]

        public async Task<IActionResult> Update(int id, [FromBody] TicketDto dto)
        {
            await _ticketRepo.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Tickets.Delete")]

        public async Task<IActionResult> Delete(int id)
        {
            await _ticketRepo.DeleteAsync(id);
            return NoContent();
        }






        // ------------------- Messages -------------------

        [HttpGet("{ticketId}/messages")]
        [Authorize(Policy = "Tickets.GetMessages")]

        public async Task<IActionResult> GetMessages(int ticketId)
        {
            var messages = await _messageRepo.GetByTicketIdAsync(ticketId);
            return Ok(messages);
        }

        [HttpPost("{ticketId}/messages")]
        [Authorize(Policy = "Tickets.AddMessage")]

        public async Task<IActionResult> AddMessage(int ticketId, [FromBody] TicketMessageDto dto)
        {
            dto.TicketId = ticketId;
            var messageId = await _messageRepo.AddAsync(dto);
            return Ok(messageId);
        

    }
}
}