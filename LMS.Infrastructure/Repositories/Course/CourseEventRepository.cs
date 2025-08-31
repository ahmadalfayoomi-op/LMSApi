using AutoMapper;
using LMS.Application.DTOs.Course;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Course;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LMS.Infrastructure.Repositories.Course
{
    public class CourseEventRepository : ICourseEventRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;


        public CourseEventRepository(AppDbContext context, IMapper mapper, IUserContextService userContext)
        {
            _context = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IEnumerable<CourseEventDto>> GetAllByCourseAsync(int courseId, CancellationToken ct = default)
        {
            var events = await _context.CourseEvents
                .Where(e => e.CourseId == courseId)
                .OrderBy(e => e.StartTime)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<CourseEventDto>>(events);
        }

        public async Task<CourseEventDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var courseEvent = await _context.CourseEvents.FindAsync(new object[] { id }, ct);
            return courseEvent == null ? null : _mapper.Map<CourseEventDto>(courseEvent);
        }

        public async Task<CourseEventDto> AddAsync(CourseEventDto courseEventDto, CancellationToken ct = default)
        {
            var courseEvent = _mapper.Map<CourseEvent>(courseEventDto);
            _context.CourseEvents.Add(courseEvent);
            await _context.SaveChangesAsync(ct);

            CourseViewLogDto viewLog = new CourseViewLogDto
            {
                StudentId = _userContext.GetStudentId(),
                CourseId = courseEvent.CourseId,
                ViewedAt = DateTime.UtcNow
            };
            await _context.CourseViewLogs.AddAsync(_mapper.Map<CourseViewLog>(viewLog), ct);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<CourseEventDto>(courseEvent);
        }

        public async Task<CourseEventDto> UpdateAsync(CourseEventDto courseEventDto, CancellationToken ct = default)
        {
            var courseEvent = await _context.CourseEvents.FindAsync(new object[] { courseEventDto.Id }, ct);
            if (courseEvent == null) throw new KeyNotFoundException("Event not found");

            _mapper.Map(courseEventDto, courseEvent);
            _context.CourseEvents.Update(courseEvent);
            CourseViewLogDto viewLog = new CourseViewLogDto
            {
                StudentId = _userContext.GetStudentId(),
                CourseId = courseEvent.CourseId,
                ViewedAt = DateTime.UtcNow
            };
            await _context.CourseViewLogs.AddAsync(_mapper.Map<CourseViewLog>(viewLog), ct);
            await _context.SaveChangesAsync(ct);

            return _mapper.Map<CourseEventDto>(courseEvent);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var courseEvent = await _context.CourseEvents.FindAsync(new object[] { id }, ct);
            if (courseEvent != null)
            {
                CourseViewLogDto viewLog = new CourseViewLogDto
                {
                    StudentId = _userContext.GetStudentId(),
                    CourseId = courseEvent.CourseId,
                    ViewedAt = DateTime.UtcNow
                };
                await _context.CourseViewLogs.AddAsync(_mapper.Map<CourseViewLog>(viewLog), ct);
                _context.CourseEvents.Remove(courseEvent);
                await _context.SaveChangesAsync(ct);
            }
        }
    }

}
