
using LMS.Application.DTOs.Course;

namespace LMS.Application.Interfaces.Course
{
    public interface IFavoriteCourseRepository
    {
        Task<IEnumerable<FavoriteCourseDto>> GetByStudentAsync(CancellationToken ct = default);
        Task<FavoriteCourseDto> AddAsync(FavoriteCourseDto favoriteDto, CancellationToken ct = default);
        Task RemoveAsync(int courseId, CancellationToken ct = default);
    }
}
