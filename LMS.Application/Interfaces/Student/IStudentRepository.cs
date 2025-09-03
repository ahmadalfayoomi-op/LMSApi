using LMS.Application.DTOs.Student;


namespace LMS.Application.Interfaces.Student
{
    public interface IStudentRepository
    {
        Task<List<StudentDto>> GetAllAsync(CancellationToken ct = default);
        Task<StudentDto?> GetByIdAsync(CancellationToken ct = default);
        Task<StudentDto?> GetByUserIdAsync(int id, CancellationToken ct = default);
        Task DeleteAsync(CancellationToken ct = default);
        Task<StudentDto> AddAsync(StudentDto student, CancellationToken ct = default);
        Task<StudentDto> UpdateAsync(StudentDto student, CancellationToken ct = default);
    }
}
