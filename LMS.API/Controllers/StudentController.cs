using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        [Authorize(Policy = "Student.View")]

        public async Task<ActionResult<List<StudentDto>>> GetAll(CancellationToken ct)
        {
            var students = await _studentRepository.GetAllAsync(ct);
            return Ok(students);
        }

        [HttpGet("me")]
        [Authorize(Policy = "Student.View")]

        public async Task<ActionResult<StudentDto>> GetMyProfile(CancellationToken ct)
        {
            var student = await _studentRepository.GetByIdAsync(ct);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpGet("user/{userId:int}")]
        [Authorize(Policy = "Student.View")]

        public async Task<ActionResult<StudentDto>> GetByUserId(int userId, CancellationToken ct)
        {
            var student = await _studentRepository.GetByUserIdAsync(userId, ct);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        [AllowAnonymous]
        [Authorize(Policy = "Student.Create")]

        public async Task<ActionResult<StudentDto>> Create([FromForm] StudentDto dto, CancellationToken ct)
        {
            var created = await _studentRepository.AddAsync(dto, ct);
            return CreatedAtAction(nameof(GetMyProfile), new { id = created.Id }, created);
        }

        [HttpPut]
        [Authorize(Policy = "Student.Update")]

        public async Task<ActionResult<StudentDto>> Update([FromForm] StudentDto dto, CancellationToken ct)
        {
            var updated = await _studentRepository.UpdateAsync(dto, ct);
            return Ok(updated);
        }

        [HttpDelete]
        [Authorize(Policy = "Student.Delete")]

        public async Task<IActionResult> Delete(CancellationToken ct)
        {
            await _studentRepository.DeleteAsync(ct);
            return NoContent();
        }
    }
}
