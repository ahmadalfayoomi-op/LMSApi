using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Student;
using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

namespace LMS.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DiscussionsController : ControllerBase
    {
        private readonly IDiscussionRepository _repo;

        public DiscussionsController(IDiscussionRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("lesson/{lessonId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllByLesson(int lessonId, CancellationToken ct)
        {
            var discussions = await _repo.GetAllByLessonAsync(lessonId, ct);
            return Ok(discussions);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var discussion = await _repo.GetByIdAsync(id, ct);
            if (discussion == null) return NotFound();
            return Ok(discussion);
        }

        [HttpPost("add")]
        [Authorize(Policy = "Discussions.Create")]
        public async Task<IActionResult> AddDiscussion([FromBody] DiscussionDto dto, CancellationToken ct)
        {
            var discussion = await _repo.AddDiscussionAsync(dto, ct);
            return Ok(discussion);
        }

        [HttpPost("reply/add")]
        [Authorize(Policy = "Discussions.Reply")]
        public async Task<IActionResult> AddReply([FromBody] DiscussionReplyDto dto, CancellationToken ct)
        {
            var reply = await _repo.AddReplyAsync(dto, ct);
            return Ok(reply);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Discussions.Delete")]
        public async Task<IActionResult> DeleteDiscussion(int id, CancellationToken ct)
        {
            await _repo.DeleteDiscussionAsync(id, ct);
            return NoContent();
        }

        [HttpDelete("reply/{id}")]
        [Authorize(Policy = "Discussions.DeleteReply")]
        public async Task<IActionResult> DeleteReply(int id, CancellationToken ct)
        {
            await _repo.DeleteReplyAsync(id, ct);
            return NoContent();
        }
    }

}
