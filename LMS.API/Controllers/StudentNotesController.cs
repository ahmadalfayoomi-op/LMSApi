using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class StudentNotesController : ControllerBase
    {
        private readonly IStudentNoteRepository _repo;

        public StudentNotesController(IStudentNoteRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Authorize(Policy = "StudentNotes.Create")]
        public async Task<ActionResult<StudentNoteDto>> Create(StudentNoteDto dto, CancellationToken ct)
        {
            var result = await _repo.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "StudentNotes.View")]
        public async Task<ActionResult<StudentNoteDto>> GetById(int id, CancellationToken ct)
        {
            var note = await _repo.GetByIdAsync(id, ct);
            if (note == null) return NotFound();
            return Ok(note);
        }

        [HttpGet]
        [Authorize(Policy = "StudentNotes.View")]
        public async Task<ActionResult<IEnumerable<StudentNoteDto>>> GetStudentNotes(CancellationToken ct)
        {
            var notes = await _repo.GetStudentNotesAsync(ct);
            return Ok(notes);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "StudentNotes.Update")]
        public async Task<ActionResult<StudentNoteDto>> Update(int id, StudentNoteDto dto, CancellationToken ct)
        {
            if (id != dto.Id) return BadRequest("Id mismatch");

            var updated = await _repo.UpdateAsync(dto, ct);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "StudentNotes.Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _repo.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
