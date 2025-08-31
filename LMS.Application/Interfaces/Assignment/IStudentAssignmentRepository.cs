using LMS.Application.DTOs.Assignment;


namespace LMS.Application.Interfaces.Assignment
{
    public interface IStudentAssignmentRepository
    {
        Task<IEnumerable<StudentAssignmentDto>> GetAllByStudentAsync(CancellationToken ct = default);
        Task<StudentAssignmentDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<StudentAssignmentDto> AddAsync(StudentAssignmentDto studentAssignmentDto, CancellationToken ct = default);
        Task<StudentAssignmentDto> UpdateAsync(StudentAssignmentDto studentAssignmentDto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }

}
