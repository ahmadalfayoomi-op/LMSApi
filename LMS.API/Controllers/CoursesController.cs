using LMS.Application.DTOs.Course;
using LMS.Application.Interfaces.Course;
using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

namespace LMS.API.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepo;

        public CoursesController(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        [HttpGet]
        [Authorize(Policy = "Courses.View")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var courses = await _courseRepo.GetAllAsync(ct);
            return Ok(courses);
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = "Courses.View")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var course = await _courseRepo.GetByIdAsync(id, ct);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        [Authorize(Policy = "Courses.Create")]
        public async Task<IActionResult> Create([FromBody] CourseDto courseDto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _courseRepo.AddAsync(courseDto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = "Courses.Update")]
        public async Task<IActionResult> Update(int id, [FromBody] CourseDto courseDto, CancellationToken ct)
        {
            if (id != courseDto.Id) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updated = await _courseRepo.UpdateAsync(courseDto, ct);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "Courses.Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _courseRepo.DeleteAsync(id, ct);
            return NoContent();
        }
    }


}
