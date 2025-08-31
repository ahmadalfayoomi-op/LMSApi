using LMS.Application.DTOs.Course;


namespace LMS.Application.Interfaces.Course
{
    public interface ICourseEventRepository
    {
        Task<IEnumerable<CourseEventDto>> GetAllByCourseAsync(int courseId, CancellationToken ct = default);
        Task<CourseEventDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<CourseEventDto> AddAsync(CourseEventDto courseEventDto, CancellationToken ct = default);
        Task<CourseEventDto> UpdateAsync(CourseEventDto courseEventDto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
