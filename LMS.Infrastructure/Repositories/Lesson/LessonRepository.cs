using AutoMapper;
using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Lesson;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories.Lesson
{
    public class LessonRepository : ILessonRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LessonRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LessonDto>> GetAllByCourseAsync(int id, CancellationToken ct = default)
        {
            var Lessons = await _context.Lessons.Where(c=>c.CourseId==id).ToListAsync(ct);
            return _mapper.Map<IEnumerable<LessonDto>>(Lessons);
        }

        public async Task<LessonDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var Lesson = await _context.Lessons.FindAsync(new object[] { id }, ct);
            return Lesson == null ? null : _mapper.Map<LessonDto>(Lesson);
        }

        public async Task<LessonDto> AddAsync(LessonDto lessonDto, CancellationToken ct = default)
        {
            var Lesson = _mapper.Map<LMS.Domain.Entities.Lesson>(lessonDto);
            _context.Lessons.Add(Lesson);
            await _context.SaveChangesAsync(ct);

            return _mapper.Map<LessonDto>(Lesson);
        }

        public async Task<LessonDto> UpdateAsync(LessonDto lessonDto, CancellationToken ct = default)
        {
            var lesson = await _context.Lessons.FindAsync(new object[] { lessonDto.Id }, ct);
            if (lesson == null) throw new KeyNotFoundException("Course not found");

            _mapper.Map(lessonDto, lesson);
            _context.Lessons.Update(lesson);
            await _context.SaveChangesAsync(ct);

            return _mapper.Map<LessonDto>(lesson);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var lesson = await _context.Lessons.FindAsync(new object[] { id }, ct);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
                await _context.SaveChangesAsync(ct);
            }
        }
    }

}
