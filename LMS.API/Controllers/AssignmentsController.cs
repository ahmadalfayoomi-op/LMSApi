using LMS.Application.DTOs.Assignment;
using LMS.Application.Interfaces.Assignment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentRepository _repo;

        public AssignmentsController(IAssignmentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("course/{courseId}")]
        [Authorize(Policy = "Assignments.View")]
        public async Task<IActionResult> GetAllByCourse(int courseId, CancellationToken ct)
        {
            var assignments = await _repo.GetAllByCourseAsync(courseId, ct);
            return Ok(assignments);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Assignments.View")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var assignment = await _repo.GetByIdAsync(id, ct);
            if (assignment == null) return NotFound();
            return Ok(assignment);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize(Policy = "Assignments.Create")]
        public async Task<IActionResult> Create([FromBody] AssignmentDto dto, CancellationToken ct)
        {
            var created = await _repo.AddAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Authorize(Policy = "Assignments.Update")]
        public async Task<IActionResult> Update(int id, [FromBody] AssignmentDto dto, CancellationToken ct)
        {
            if (id != dto.Id) return BadRequest();
            var updated = await _repo.UpdateAsync(dto, ct);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [Authorize(Policy = "Assignments.Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _repo.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
