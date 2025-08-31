using LMS.Application.DTOs.Others;
using LMS.Application.Interfaces.Others;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("api/[controller]")]
    public class BadgesController : ControllerBase
    {
        private readonly IBadgeRepository _repo;

        public BadgesController(IBadgeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Authorize(Policy = "Badges.View")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var badges = await _repo.GetAllAsync(ct);
            return Ok(badges);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Badges.View")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var badge = await _repo.GetByIdAsync(id, ct);
            if (badge == null) return NotFound();
            return Ok(badge);
        }

        [HttpPost]
        [Authorize(Policy = "Badges.Create")]
        public async Task<IActionResult> Create([FromBody] BadgeDto dto, CancellationToken ct)
        {
            var created = await _repo.AddAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Badges.Update")]
        public async Task<IActionResult> Update(int id, [FromBody] BadgeDto dto, CancellationToken ct)
        {
            if (id != dto.Id) return BadRequest();
            var updated = await _repo.UpdateAsync(dto, ct);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Badges.Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _repo.DeleteAsync(id, ct);
            return NoContent();
        }
    }

}
