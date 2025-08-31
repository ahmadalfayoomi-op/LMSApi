using AutoMapper;
using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Student;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories.Student
{
    public class StudentActivityLogRepository : IStudentActivityLogRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;


        public StudentActivityLogRepository(AppDbContext context, IMapper mapper, IUserContextService userContext)
        {
            _context = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IEnumerable<StudentActivityLogDto>> GetByStudentAsync(CancellationToken ct = default)
        {
            int studentId = _userContext.GetUserId();
            var logs = await _context.StudentActivityLogs
                .Where(l => l.StudentId == studentId)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<StudentActivityLogDto>>(logs);
        }

        public async Task<StudentActivityLogDto> AddAsync(StudentActivityLogDto logDto, CancellationToken ct = default)
        {
            var entity = _mapper.Map<LMS.Domain.Entities.StudentActivityLog>(logDto);
            _context.StudentActivityLogs.Add(entity);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<StudentActivityLogDto>(entity);
        }
    }

}
