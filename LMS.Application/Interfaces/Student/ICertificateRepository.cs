using LMS.Application.DTOs.Student;

namespace LMS.Application.Interfaces.Student
{
    public interface ICertificateRepository
    {
        Task<IEnumerable<CertificateDto>> GetAllByStudentAsync(CancellationToken ct = default);
        Task<CertificateDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<CertificateDto> CreateCertificateAsync(int courseId, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }

}
