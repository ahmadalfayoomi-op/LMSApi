using LMS.Application.DTOs.Course;


namespace LMS.Application.Interfaces.Course
{
    public interface ICourseReviewRepository
    {
        Task<IEnumerable<CourseReviewDto>> GetAllByCourseAsync(int courseId, CancellationToken ct = default);
        Task<CourseReviewDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<CourseReviewDto> AddAsync(CourseReviewDto reviewDto, CancellationToken ct = default);
        Task<CourseReviewDto> UpdateAsync(CourseReviewDto reviewDto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
