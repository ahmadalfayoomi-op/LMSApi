using LMS.Application.DTOs.Student;


namespace LMS.Application.Interfaces.Lesson
{
    public interface ILessonRepository
    {
        Task<LessonDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IEnumerable<LessonDto>> GetAllByCourseAsync(int courseId, CancellationToken ct = default);
        Task<LessonDto> AddAsync(LessonDto lesson, CancellationToken ct = default);
        Task<LessonDto> UpdateAsync(LessonDto lesson, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
