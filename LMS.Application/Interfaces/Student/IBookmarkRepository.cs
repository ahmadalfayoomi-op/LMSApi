using LMS.Application.DTOs.Student;


namespace LMS.Application.Interfaces.Student
{
    public interface IBookmarkRepository
    {
        Task<IEnumerable<BookmarkDto>> GetAllByStudentAsync(CancellationToken ct = default);
        Task<BookmarkDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<BookmarkDto> AddAsync(BookmarkDto bookmarkDto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }

}
