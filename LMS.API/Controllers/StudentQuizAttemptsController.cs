using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class StudentQuizAttemptsController : ControllerBase
    {
        private readonly IStudentQuizAttemptRepository _attemptRepository;

        public StudentQuizAttemptsController(IStudentQuizAttemptRepository attemptRepository)
        {
            _attemptRepository = attemptRepository;
        }


        [HttpPost("start")]
        [Authorize(Policy = "StudentQuizAttempts.Create")]
        public async Task<ActionResult<StudentQuizAttemptDto>> StartAttempt([FromQuery] int quizId, CancellationToken ct)
        {
            var attempt = await _attemptRepository.StartAttemptAsync(quizId, ct);
            return Ok(attempt);
        }


        [HttpPost("submit")]
        [Authorize(Policy = "StudentQuizAttempts.Update")]
        public async Task<ActionResult<StudentQuizAttemptDto>> SubmitAttempt([FromBody] StudentQuizAttemptDto attemptDto, CancellationToken ct)
        {
            var result = await _attemptRepository.SubmitAttemptAsync(attemptDto, ct);
            return Ok(result);
        }


        [HttpGet("{attemptId}")]
        [Authorize(Policy = "StudentQuizAttempts.View")]
        public async Task<ActionResult<StudentQuizAttemptDto>> GetAttempt(int attemptId, CancellationToken ct)
        {
            var attempt = await _attemptRepository.GetAttemptAsync(attemptId, ct);
            if (attempt == null) return NotFound();
            return Ok(attempt);
        }
    }
}
