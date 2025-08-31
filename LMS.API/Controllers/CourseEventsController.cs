using LMS.Application.DTOs.Course;
using LMS.Application.Interfaces.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LMS.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CourseEventsController : ControllerBase
    {
        private readonly ICourseEventRepository _repo;

        public CourseEventsController(ICourseEventRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("course/{courseId}")]
        [Authorize(Policy = "CourseEvents.View")]
        public async Task<IActionResult> GetAllByCourse(int courseId, CancellationToken ct)
        {
            var events = await _repo.GetAllByCourseAsync(courseId, ct);
            return Ok(events);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "CourseEvents.View")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var courseEvent = await _repo.GetByIdAsync(id, ct);
            if (courseEvent == null) return NotFound();
            return Ok(courseEvent);
        }

        [HttpPost("add")]
        [Authorize(Policy = "CourseEvents.Create")]
        public async Task<IActionResult> Add([FromBody] CourseEventDto dto, CancellationToken ct)
        {
            var courseEvent = await _repo.AddAsync(dto, ct);
            return Ok(courseEvent);
        }

        [HttpPut("update")]
        [Authorize(Policy = "CourseEvents.Update")]
        public async Task<IActionResult> Update([FromBody] CourseEventDto dto, CancellationToken ct)
        {
            var courseEvent = await _repo.UpdateAsync(dto, ct);
            return Ok(courseEvent);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Policy = "CourseEvents.Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _repo.DeleteAsync(id, ct);
            return NoContent();
        }
    }


}
