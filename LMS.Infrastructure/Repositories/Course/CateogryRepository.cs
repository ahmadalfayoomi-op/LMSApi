

using AutoMapper;
using LMS.Application.DTOs.Course;
using LMS.Application.Interfaces.Course;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Course
{
    public class CateogryRepository : ICateogryRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public CateogryRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAllAsync(CancellationToken ct = default)
        {
            var categories = await _db.Categories.ToListAsync(ct);
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var category = await _db.Categories.FindAsync(new object[] { id }, ct);
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }
        public async Task<CategoryDto> AddAsync(CategoryDto categoryDto, CancellationToken ct = default)
        {
            var category = _mapper.Map<Category>(categoryDto); 
            _db.Categories.Add(category);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateAsync(CategoryDto categoryDto, CancellationToken ct = default)
        {
            var category = await _db.Categories.FindAsync(new object[] { categoryDto.Id }, ct);
            if (category == null) throw new KeyNotFoundException("Category not found");

            _mapper.Map(categoryDto, category);
            _db.Categories.Update(category);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync(ct);
            }
        }
    }
}
