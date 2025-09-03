

using AutoMapper;
using LMS.Application.DTOs.Course;
using LMS.Application.Interfaces.Course;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Course
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public CourseRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<CourseDto>> GetAllAsync(CancellationToken ct = default)
        {
            var Courses = await _db.Courses.ToListAsync(ct);
            return _mapper.Map<List<CourseDto>>(Courses);
        }

        public async Task<CourseDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var Course = await _db.Courses.Include(c => c.Lessons).FirstOrDefaultAsync(c => c.Id == id, ct);
            return Course == null ? null : _mapper.Map<CourseDto>(Course);
        }
        public async Task<CourseDto> AddAsync(CourseDto CourseDto, CancellationToken ct = default)
        {
            var Course = _mapper.Map<LMS.Domain.Entities.Course>(CourseDto); 
            _db.Courses.Add(Course);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<CourseDto>(Course);
        }

        public async Task<CourseDto> UpdateAsync(CourseDto CourseDto, CancellationToken ct = default)
        {
            var Course = await _db.Courses.FindAsync(new object[] { CourseDto.Id }, ct);
            if (Course == null) throw new KeyNotFoundException("Course not found");

            _mapper.Map(CourseDto, Course);
            _db.Courses.Update(Course);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<CourseDto>(Course);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var Course = await _db.Courses.FindAsync(id);
            if (Course != null)
            {
                _db.Courses.Remove(Course);
                await _db.SaveChangesAsync(ct);
            }
        }
    }
}
