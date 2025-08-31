using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Lesson;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Student")] // Only Admins and Students
    public class LessonsController : ControllerBase
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonsController(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        // GET: api/lessons/course/5
        [HttpGet("course/{courseId:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetAllByCourse(int courseId, CancellationToken ct)
        {
            var lessons = await _lessonRepository.GetAllByCourseAsync(courseId, ct);
            return Ok(lessons);
        }

        // GET: api/lessons/5
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<LessonDto>> GetById(int id, CancellationToken ct)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id, ct);
            if (lesson == null) return NotFound();
            return Ok(lesson);
        }

        // POST: api/lessons
        [HttpPost]
        [Authorize(Policy = "Lessons.Create")]
        public async Task<ActionResult<LessonDto>> Create([FromBody] LessonDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _lessonRepository.AddAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/lessons/5
        [HttpPut("{id:int}")]
        [Authorize(Policy = "Lessons.Update")]
        public async Task<ActionResult<LessonDto>> Update(int id, [FromBody] LessonDto dto, CancellationToken ct)
        {
            if (id != dto.Id) return BadRequest("Mismatched lesson ID");

            var updated = await _lessonRepository.UpdateAsync(dto, ct);
            return Ok(updated);
        }

        // DELETE: api/lessons/5
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "Lessons.Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _lessonRepository.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
