using LMS.Application.DTOs.Student;


namespace LMS.Application.Interfaces.Student
{
    public interface IStudentNoteRepository
    {
        Task<StudentNoteDto> CreateAsync(StudentNoteDto dto, CancellationToken ct = default);
        Task<StudentNoteDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IEnumerable<StudentNoteDto>> GetStudentNotesAsync(CancellationToken ct = default);
        Task<StudentNoteDto> UpdateAsync(StudentNoteDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
