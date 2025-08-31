using LMS.Application.DTOs.Quizzes;
using LMS.Application.Interfaces.Quizzes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Student")] // Only Admins and Students
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizRepository _quizRepository;

        public QuizzesController(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        [HttpGet("course/{courseId}")]
        [Authorize(Policy = "Quizzes.View")]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetByCourse(int courseId, CancellationToken ct)
        {
            var quizzes = await _quizRepository.GetAllByCourseAsync(courseId, ct);
            return Ok(quizzes);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Quizzes.View")]
        public async Task<ActionResult<QuizDto>> GetById(int id, CancellationToken ct)
        {
            var quiz = await _quizRepository.GetByIdAsync(id, ct);
            if (quiz == null) return NotFound();
            return Ok(quiz);
        }

        [HttpPost]
        [Authorize(Policy = "Quizzes.Create")]
        public async Task<ActionResult<QuizDto>> Create(QuizDto quizDto, CancellationToken ct)
        {
            var created = await _quizRepository.AddAsync(quizDto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Quizzes.Update")]
        public async Task<ActionResult<QuizDto>> Update(int id, QuizDto quizDto, CancellationToken ct)
        {
            if (id != quizDto.Id) return BadRequest("ID mismatch");
            var updated = await _quizRepository.UpdateAsync(quizDto, ct);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Quizzes.Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _quizRepository.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
