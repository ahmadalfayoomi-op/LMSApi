using LMS.Application.DTOs.Student;


namespace LMS.Application.Interfaces.Student
{
    public interface IStudentActivityLogRepository
    {
        Task<IEnumerable<StudentActivityLogDto>> GetByStudentAsync(CancellationToken ct = default);
        Task<StudentActivityLogDto> AddAsync(StudentActivityLogDto logDto, CancellationToken ct = default);
    }
}
