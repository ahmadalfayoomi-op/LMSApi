using LMS.Application.DTOs.Course;


namespace LMS.Application.Interfaces.Course
{
    public interface ICateogryRepository
    {
        Task<List<CategoryDto>> GetAllAsync(CancellationToken ct = default);
        Task<CategoryDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task<CategoryDto> AddAsync(CategoryDto category, CancellationToken ct = default);
        Task<CategoryDto> UpdateAsync(CategoryDto category, CancellationToken ct = default);
    }
}
