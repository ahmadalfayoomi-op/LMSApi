using LMS.Application.DTOs.Course;


namespace LMS.Application.Interfaces.Course
{
    public interface ICourseViewLogRepository
    {
        Task<IEnumerable<CourseViewLogDto>> GetByStudentAsync(CancellationToken ct = default);
        Task<IEnumerable<CourseViewLogDto>> GetByCourseAsync(int courseId, CancellationToken ct = default);
        Task<CourseViewLogDto> AddAsync(CourseViewLogDto logDto, CancellationToken ct = default);
    }
}
