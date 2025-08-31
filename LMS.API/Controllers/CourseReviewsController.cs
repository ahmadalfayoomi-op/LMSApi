using LMS.Application.DTOs.Course;
using LMS.Application.Interfaces.Course;
using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

namespace LMS.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CourseReviewsController : ControllerBase
    {
        private readonly ICourseReviewRepository _repo;

        public CourseReviewsController(ICourseReviewRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("course/{courseId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllByCourse(int courseId, CancellationToken ct)
        {
            var reviews = await _repo.GetAllByCourseAsync(courseId, ct);
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var review = await _repo.GetByIdAsync(id, ct);
            if (review == null) return NotFound();
            return Ok(review);
        }

        [HttpPost("add")]
        [Authorize(Policy = "CourseReviews.Create")]
        public async Task<IActionResult> Add([FromBody] CourseReviewDto dto, CancellationToken ct)
        {
            var review = await _repo.AddAsync(dto, ct);
            return Ok(review);
        }

        [HttpPut("update")]
        [Authorize(Policy = "CourseReviews.Update")]
        public async Task<IActionResult> Update([FromBody] CourseReviewDto dto, CancellationToken ct)
        {
            var review = await _repo.UpdateAsync(dto, ct);
            return Ok(review);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Policy = "CourseReviews.Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _repo.DeleteAsync(id, ct);
            return NoContent();
        }
    }


}
