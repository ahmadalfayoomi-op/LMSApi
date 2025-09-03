using LMS.Application.DTOs.Instructor;
using LMS.Application.Interfaces.Instructor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorController(IInstructorRepository InstructorRepository, IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }


        [HttpGet]
        [Authorize(Policy = "Instructor.View")]

        public async Task<ActionResult<List<InstructorDto>>> GetAll(CancellationToken ct)
        {
            var Instructors = await _instructorRepository.GetAllAsync(ct);
            return Ok(Instructors);
        }

        [HttpGet("me")]
        [Authorize(Policy = "Instructor.View")]

        public async Task<ActionResult<InstructorDto>> GetMyProfile(CancellationToken ct)
        {
            var Instructor = await _instructorRepository.GetByIdAsync(ct);
            if (Instructor == null) return NotFound();
            return Ok(Instructor);
        }

        [HttpGet("user/{userId:int}")]
        [Authorize(Policy = "Instructor.View")]

        public async Task<ActionResult<InstructorDto>> GetByUserId(int userId, CancellationToken ct)
        {
            var Instructor = await _instructorRepository.GetByUserIdAsync(userId, ct);
            if (Instructor == null) return NotFound();
            return Ok(Instructor);
        }

        [HttpPost]
        [Authorize(Policy = "Instructor.Create")]
        public async Task<ActionResult<InstructorDto>> Create([FromForm] InstructorDto dto, CancellationToken ct)
        {
            var created = await _instructorRepository.AddAsync(dto, ct);
            return CreatedAtAction(nameof(GetMyProfile), new { id = created.Id }, created);
        }

        [HttpPut]
        [Authorize(Policy = "Instructor.Update")]

        public async Task<ActionResult<InstructorDto>> Update([FromForm] InstructorDto dto, CancellationToken ct)
        {
            var updated = await _instructorRepository.UpdateAsync(dto, ct);
            return Ok(updated);
        }

        [HttpDelete]
        [Authorize(Policy = "Instructor.Delete")]

        public async Task<IActionResult> Delete(CancellationToken ct)
        {
            await _instructorRepository.DeleteAsync(ct);
            return NoContent();
        }
    }
}
