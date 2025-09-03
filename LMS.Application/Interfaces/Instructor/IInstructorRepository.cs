using LMS.Application.DTOs.Instructor;


namespace LMS.Application.Interfaces.Instructor
{
    public interface IInstructorRepository
    {
        Task<List<InstructorDto>> GetAllAsync(CancellationToken ct = default);
        Task<InstructorDto?> GetByIdAsync(CancellationToken ct = default);
        Task<InstructorDto?> GetByUserIdAsync(int id, CancellationToken ct = default);
        Task DeleteAsync( CancellationToken ct = default);
        Task<InstructorDto> AddAsync(InstructorDto student, CancellationToken ct = default);
        Task<InstructorDto> UpdateAsync(InstructorDto student, CancellationToken ct = default);
    }
}
