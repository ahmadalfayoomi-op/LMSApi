using AutoMapper;
using LMS.Application.DTOs.Course;
using LMS.Application.DTOs.Others;
using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Course;
using LMS.Application.Interfaces.Others;
using LMS.Application.Interfaces.Student;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories.Course
{
    public class CourseViewLogRepository : ICourseViewLogRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;


        public CourseViewLogRepository(AppDbContext context, IMapper mapper, IUserContextService userContext)
        {
            _context = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IEnumerable<CourseViewLogDto>> GetByStudentAsync(CancellationToken ct = default)
        {
            int studentId=_userContext.GetUserId();
            var logs = await _context.CourseViewLogs
                .Where(l => l.StudentId == studentId)
                .OrderByDescending(l => l.ViewedAt)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<CourseViewLogDto>>(logs);
        }

        public async Task<IEnumerable<CourseViewLogDto>> GetByCourseAsync(int courseId, CancellationToken ct = default)
        {
            var logs = await _context.CourseViewLogs
                .Where(l => l.CourseId == courseId)
                .OrderByDescending(l => l.ViewedAt)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<CourseViewLogDto>>(logs);
        }

        public async Task<CourseViewLogDto> AddAsync(CourseViewLogDto logDto, CancellationToken ct = default)
        {
            var entity = _mapper.Map<LMS.Domain.Entities.CourseViewLog>(logDto);
            _context.CourseViewLogs.Add(entity);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<CourseViewLogDto>(entity);
        }
    }
}
