using LMS.Application.DTOs.Course;


namespace LMS.Application.Interfaces.Course
{
    public interface IEnrollmentRepository
    {
        Task<List<EnrollmentDto>> GetAllAsync(CancellationToken ct = default);
        Task<EnrollmentDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task<EnrollmentDto> AddAsync(EnrollmentDto enrollment, CancellationToken ct = default);
        Task<EnrollmentDto> UpdateAsync(EnrollmentDto enrollment, CancellationToken ct = default);
    }
}
