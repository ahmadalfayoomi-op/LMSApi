using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BookmarksController : ControllerBase
    {
        private readonly IBookmarkRepository _repo;

        public BookmarksController(IBookmarkRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("student")]
        [Authorize(Policy = "Bookmarks.View")]
        public async Task<IActionResult> GetAllByStudent(CancellationToken ct)
        {
            var bookmarks = await _repo.GetAllByStudentAsync(ct);
            return Ok(bookmarks);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Bookmarks.View")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var bookmark = await _repo.GetByIdAsync(id, ct);
            if (bookmark == null) return NotFound();
            return Ok(bookmark);
        }

        [HttpPost]
        [Authorize(Policy = "Bookmarks.Create")]
        public async Task<IActionResult> Create([FromBody] BookmarkDto dto, CancellationToken ct)
        {
            var created = await _repo.AddAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Bookmarks.Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _repo.DeleteAsync(id, ct);
            return NoContent();
        }
    }


}
