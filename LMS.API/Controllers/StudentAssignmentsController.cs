using LMS.Application.DTOs.Assignment;
using LMS.Application.Interfaces.Assignment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class StudentAssignmentsController : ControllerBase
    {
        private readonly IStudentAssignmentRepository _repo;

        public StudentAssignmentsController(IStudentAssignmentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("student")]
        [Authorize(Policy = "StudentAssignments.View")]
        public async Task<IActionResult> GetAllByStudent(CancellationToken ct)
        {
            var assignments = await _repo.GetAllByStudentAsync(ct);
            return Ok(assignments);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "StudentAssignments.View")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var assignment = await _repo.GetByIdAsync(id, ct);
            if (assignment == null) return NotFound();
            return Ok(assignment);
        }

        [HttpPost]
        [Authorize(Policy = "StudentAssignments.Submit")]
        public async Task<IActionResult> Submit([FromBody] StudentAssignmentDto dto, CancellationToken ct)
        {
            var created = await _repo.AddAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "StudentAssignments.Update")]
        public async Task<IActionResult> UpdateSubmission(int id, [FromBody] StudentAssignmentDto dto, CancellationToken ct)
        {
            if (id != dto.Id) return BadRequest();
            var updated = await _repo.UpdateAsync(dto, ct);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "StudentAssignments.Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _repo.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
