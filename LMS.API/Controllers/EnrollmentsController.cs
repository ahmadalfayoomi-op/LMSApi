using LMS.Application.DTOs.Course;
using LMS.Application.Interfaces.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LMS.API.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentRepository _enrollmentRepo;

        public EnrollmentsController(IEnrollmentRepository enrollmentRepo)
        {
            _enrollmentRepo = enrollmentRepo;
        }

        [HttpGet]
        [Authorize(Policy = "Enrollments.View")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var enrollments = await _enrollmentRepo.GetAllAsync(ct);
            return Ok(enrollments);
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = "Enrollments.View")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var enrollment = await _enrollmentRepo.GetByIdAsync(id, ct);
            if (enrollment == null) return NotFound();
            return Ok(enrollment);
        }

        [HttpPost]
        [Authorize(Policy = "Enrollments.Create")]
        public async Task<IActionResult> Create([FromBody] EnrollmentDto enrollmentDto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _enrollmentRepo.AddAsync(enrollmentDto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = "Enrollments.Update")]
        public async Task<IActionResult> Update(int id, [FromBody] EnrollmentDto enrollmentDto, CancellationToken ct)
        {
            if (id != enrollmentDto.Id) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updated = await _enrollmentRepo.UpdateAsync(enrollmentDto, ct);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "Enrollments.Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _enrollmentRepo.DeleteAsync(id, ct);
            return NoContent();
        }
    }


}
