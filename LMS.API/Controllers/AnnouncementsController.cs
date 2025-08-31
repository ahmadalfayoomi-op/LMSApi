using LMS.Application.DTOs.Others;
using LMS.Application.Interfaces.Others;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementRepository _repo;

        public AnnouncementsController(IAnnouncementRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("course/{courseId}")]
        [Authorize(Policy = "Announcements.View")]
        public async Task<IActionResult> GetAllByCourse(int courseId, CancellationToken ct)
        {
            var announcements = await _repo.GetAllByCourseAsync(courseId, ct);
            return Ok(announcements);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var announcement = await _repo.GetByIdAsync(id, ct);
            if (announcement == null) return NotFound();
            return Ok(announcement);
        }

        [HttpPost]
        [Authorize(Policy = "Announcements.Create")]
        public async Task<IActionResult> Create([FromBody] AnnouncementDto announcementDto, CancellationToken ct)
        {
            var created = await _repo.AddAsync(announcementDto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Announcements.Update")]
        public async Task<IActionResult> Update(int id, [FromBody] AnnouncementDto announcementDto, CancellationToken ct)
        {
            if (id != announcementDto.Id) return BadRequest();
            var updated = await _repo.UpdateAsync(announcementDto, ct);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Announcements.Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _repo.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
