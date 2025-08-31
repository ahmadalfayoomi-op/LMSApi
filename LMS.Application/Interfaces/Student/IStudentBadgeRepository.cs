using LMS.Application.DTOs.Student;


namespace LMS.Application.Interfaces.Student
{
    public interface IStudentBadgeRepository
    {
        Task<StudentBadgeDto> AwardBadgeAsync(StudentBadgeDto dto, CancellationToken ct = default);
        Task<IEnumerable<StudentBadgeDto>> GetStudentBadgesAsync(CancellationToken ct = default);
        Task<StudentBadgeDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<StudentBadgeDto> UpdateAsync(StudentBadgeDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
