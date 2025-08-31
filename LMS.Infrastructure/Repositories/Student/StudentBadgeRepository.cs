using AutoMapper;
using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Student;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories.Student
{
    public class StudentBadgeRepository : IStudentBadgeRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;




        public StudentBadgeRepository(AppDbContext db, IMapper mapper, IUserContextService userContext)
        {
            _db = db;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<StudentBadgeDto> AwardBadgeAsync(StudentBadgeDto dto, CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var entity = _mapper.Map<StudentBadge>(dto);
            entity.StudentId = studentId;

            _db.StudentBadges.Add(entity);
            await _db.SaveChangesAsync(ct);

            StudentActivityLogDto logDto = new StudentActivityLogDto
            {
                StudentId = studentId,
                ActivityType = "BadgeAwarded",
                Description = $"Awarded badge {entity.Badge.Name}",
                CreatedAt = DateTime.UtcNow
            };

            var activitylog = _mapper.Map<StudentActivityLog>(logDto);
            activitylog.StudentId = studentId;

            await _db.StudentActivityLogs.AddAsync(activitylog);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<StudentBadgeDto>(entity);
        }

        public async Task<IEnumerable<StudentBadgeDto>> GetStudentBadgesAsync(CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var entities = await _db.StudentBadges
                .Where(sb => sb.StudentId == studentId)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<StudentBadgeDto>>(entities);
        }

        public async Task<StudentBadgeDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _db.StudentBadges.FindAsync(new object[] { id }, ct);
            return entity == null ? null : _mapper.Map<StudentBadgeDto>(entity);
        }

        public async Task<StudentBadgeDto> UpdateAsync(StudentBadgeDto dto, CancellationToken ct = default)
        {
            var entity = await _db.StudentBadges.FindAsync(new object[] { dto.Id }, ct);
            if (entity == null) throw new KeyNotFoundException("StudentBadge not found");

            int studentId = _userContext.GetStudentId();

            _mapper.Map(dto, entity);
            entity.StudentId = studentId;
            _db.StudentBadges.Update(entity);

            StudentActivityLogDto logDto = new StudentActivityLogDto
            {
                StudentId = studentId,
                ActivityType = "BadgeUpdated",
                Description = $"Updated Awarded badge {entity.Badge.Name}",
                CreatedAt = DateTime.UtcNow
            };

            var activitylog = _mapper.Map<StudentActivityLog>(logDto);
            activitylog.StudentId = studentId;

            await _db.StudentActivityLogs.AddAsync(activitylog);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<StudentBadgeDto>(entity);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _db.StudentBadges.FindAsync(new object[] { id }, ct);
            if (entity != null)
            {

                StudentActivityLogDto logDto = new StudentActivityLogDto
                {
                    StudentId = entity.StudentId,
                    ActivityType = "BadgeRemoved",
                    Description = $"Remove Awarded badge {entity.Badge.Name}",
                    CreatedAt = DateTime.UtcNow
                };

                var activitylog = _mapper.Map<StudentActivityLog>(logDto);
                activitylog.StudentId = entity.StudentId;

                await _db.StudentActivityLogs.AddAsync(activitylog);
                _db.StudentBadges.Remove(entity);
                await _db.SaveChangesAsync(ct);
            }
        }
    }

}
