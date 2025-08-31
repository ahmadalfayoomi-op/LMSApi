using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Student;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("api/[controller]")]
    public class CertificatesController : ControllerBase
    {
        private readonly ICertificateRepository _repo;

        public CertificatesController(ICertificateRepository repo)
        {
            _repo = repo;
        }

        // Get all certificates for a student
        [HttpGet("student")]
        [Authorize(Policy = "Certificates.View")]
        public async Task<IActionResult> GetAllByStudent(CancellationToken ct)
        {
            var certificates = await _repo.GetAllByStudentAsync(ct);
            return Ok(certificates);
        }

        // Get certificate by Id
        [HttpGet("{id}")]
        [Authorize(Policy = "Certificates.View")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var certificate = await _repo.GetByIdAsync(id, ct);
            if (certificate == null) return NotFound();
            return Ok(certificate);
        }

        // Create a certificate for a student & course
        [HttpPost("create")]
        [Authorize(Policy = "Certificates.Create")]
        public async Task<IActionResult> CreateCertificate(int courseId, CancellationToken ct)
        {
            var certificate = await _repo.CreateCertificateAsync(courseId, ct);
            return Ok(certificate);
        }

        // Delete a certificate
        [HttpDelete("{id}")]
        [Authorize(Policy = "Certificates.Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _repo.DeleteAsync(id, ct);
            return NoContent();
        }
    }


}
