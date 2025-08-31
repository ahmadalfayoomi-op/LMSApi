using LMS.Application.DTOs.Course;


namespace LMS.Application.Interfaces.Course
{
    public interface ICourseRepository
    {
        Task<List<CourseDto>> GetAllAsync(CancellationToken ct = default);
        Task<CourseDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task<CourseDto> AddAsync(CourseDto course, CancellationToken ct = default);
        Task<CourseDto> UpdateAsync(CourseDto course, CancellationToken ct = default);
    }
}
