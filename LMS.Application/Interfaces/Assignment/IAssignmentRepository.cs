using LMS.Application.DTOs.Assignment;


namespace LMS.Application.Interfaces.Assignment
{
    public interface IAssignmentRepository
    {
        Task<IEnumerable<AssignmentDto>> GetAllByCourseAsync(int courseId, CancellationToken ct = default);
        Task<AssignmentDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<AssignmentDto> AddAsync(AssignmentDto assignmentDto, CancellationToken ct = default);
        Task<AssignmentDto> UpdateAsync(AssignmentDto assignmentDto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
