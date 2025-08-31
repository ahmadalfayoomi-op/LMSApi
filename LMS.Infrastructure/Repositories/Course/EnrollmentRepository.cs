using AutoMapper;
using LMS.Application.DTOs.Course;
using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Course;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Course
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;

        public EnrollmentRepository(AppDbContext db, IMapper mapper, IUserContextService userContext)
        {
            _db = db;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<List<EnrollmentDto>> GetAllAsync(CancellationToken ct = default)
        {
            var Enrollments = await _db.Enrollments.ToListAsync(ct);
            return _mapper.Map<List<EnrollmentDto>>(Enrollments);
        }

        public async Task<EnrollmentDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var Enrollment = await _db.Enrollments.FindAsync(new object[] { id }, ct);
            return Enrollment == null ? null : _mapper.Map<EnrollmentDto>(Enrollment);
        }
        public async Task<EnrollmentDto> AddAsync(EnrollmentDto EnrollmentDto, CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var Enrollment = _mapper.Map<Enrollment>(EnrollmentDto); 
            Enrollment.StudentId = studentId;
            _db.Enrollments.Add(Enrollment);
            await _db.SaveChangesAsync(ct);

            StudentActivityLogDto log = new StudentActivityLogDto
            {
                StudentId = studentId,
                ActivityType = "Enrollment",
                Description = $"Enrolled in course {Enrollment.Course.Title}",
                CreatedAt = DateTime.UtcNow
            };
            _db.StudentActivityLogs.Add(_mapper.Map<StudentActivityLog>(log));
            await _db.SaveChangesAsync(ct);
            return _mapper.Map<EnrollmentDto>(Enrollment);
        }

        public async Task<EnrollmentDto> UpdateAsync(EnrollmentDto EnrollmentDto, CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var Enrollment = await _db.Enrollments.FindAsync(new object[] { EnrollmentDto.Id }, ct);
            if (Enrollment == null) throw new KeyNotFoundException("Enrollment not found");

            _mapper.Map(EnrollmentDto, Enrollment);
            Enrollment.StudentId = studentId;
            _db.Enrollments.Update(Enrollment);
            StudentActivityLogDto log = new StudentActivityLogDto
            {
                StudentId = studentId,
                ActivityType = "Enrollment Update",
                Description = $"Updated enrollment in course {Enrollment.Course.Title}",
                CreatedAt = DateTime.UtcNow
            };
            _db.StudentActivityLogs.Add(_mapper.Map<StudentActivityLog>(log));
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<EnrollmentDto>(Enrollment);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var Enrollment = await _db.Enrollments.FindAsync(id);
            if (Enrollment != null)
            {
                StudentActivityLogDto log = new StudentActivityLogDto
                {
                    StudentId = Enrollment.StudentId,
                    ActivityType = "Enrollment Deletion",
                    Description = $"Deleted enrollment in course {Enrollment.Course.Title}",
                    CreatedAt = DateTime.UtcNow
                };
                _db.StudentActivityLogs.Add(_mapper.Map<StudentActivityLog>(log));
                _db.Enrollments.Remove(Enrollment);
                await _db.SaveChangesAsync(ct);
            }
        }
    }
}
