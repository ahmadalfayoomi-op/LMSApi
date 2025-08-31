using AutoMapper;
using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Student;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LMS.Infrastructure.Repositories.Student
{
    public class StudentProgressRepository : IStudentProgressRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;


        public StudentProgressRepository(AppDbContext context, IMapper mapper, IUserContextService userContext)
        {
            _context = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<StudentProgressDto> MarkLessonCompletedAsync(int lessonId, CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var progress = await _context.StudentProgress
                .FirstOrDefaultAsync(p => p.StudentId == studentId && p.LessonId == lessonId, ct);

            if (progress == null)
            {
                progress = new StudentProgress
                {
                    StudentId = studentId,
                    LessonId = lessonId,
                    IsCompleted = true
                };
                _context.StudentProgress.Add(progress);
            }
            else
            {
                progress.IsCompleted = true;
            }

            await _context.SaveChangesAsync(ct);

            StudentActivityLogDto logDto = new StudentActivityLogDto
            {
                StudentId = studentId,
                ActivityType = "CompleteLesson",
                Description = $"Completed lesson with {progress?.Lesson?.Title}",
                CreatedAt = DateTime.UtcNow
            };
            var activitylog = _mapper.Map<StudentActivityLog>(logDto);
            await _context.StudentActivityLogs.AddAsync(activitylog);
            await _context.SaveChangesAsync(ct);

            return _mapper.Map<StudentProgressDto>(progress);
        }

        public async Task<IEnumerable<StudentProgressDto>> GetStudentProgressAsync(CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var progressList = await _context.StudentProgress
                .Where(p => p.StudentId == studentId)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<StudentProgressDto>>(progressList);
        }
    }

}
