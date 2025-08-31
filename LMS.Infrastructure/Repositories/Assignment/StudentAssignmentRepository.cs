using AutoMapper;
using LMS.Application.DTOs.Assignment;
using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Assignment;
using LMS.Application.Interfaces.Configuration;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LMS.Infrastructure.Repositories.Assignment
{
    public class StudentAssignmentRepository : IStudentAssignmentRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;


        public StudentAssignmentRepository(AppDbContext context, IMapper mapper, IUserContextService userContext)
        {
            _context = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IEnumerable<StudentAssignmentDto>> GetAllByStudentAsync(CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var assignments = await _context.StudentAssignments
                .Where(sa => sa.StudentId == studentId)
                .OrderByDescending(sa => sa.CreatedAt)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<StudentAssignmentDto>>(assignments);
        }

        public async Task<StudentAssignmentDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var assignment = await _context.StudentAssignments.FindAsync(new object[] { id }, ct);
            return assignment == null ? null : _mapper.Map<StudentAssignmentDto>(assignment);
        }

        public async Task<StudentAssignmentDto> AddAsync(StudentAssignmentDto studentAssignmentDto, CancellationToken ct = default)
        {
            var studentAssignment = _mapper.Map<StudentAssignment>(studentAssignmentDto);
            studentAssignment.StudentId = _userContext.GetStudentId();
            _context.StudentAssignments.Add(studentAssignment);
            await _context.SaveChangesAsync(ct);

            StudentActivityLogDto logDto = new StudentActivityLogDto
            {
                StudentId = studentAssignment.StudentId,
                ActivityType = "Submitted Assignment",
                Description = $"Submitted assignment with {studentAssignment.Assignment.Title}",
                CreatedAt = DateTime.UtcNow
            };

            var activitylog = _mapper.Map<StudentActivityLog>(logDto);
            await _context.StudentActivityLogs.AddAsync(activitylog);
            await _context.SaveChangesAsync(ct);

            return _mapper.Map<StudentAssignmentDto>(studentAssignment);
        }

        public async Task<StudentAssignmentDto> UpdateAsync(StudentAssignmentDto studentAssignmentDto, CancellationToken ct = default)
        {
            var studentAssignment = await _context.StudentAssignments.FindAsync(new object[] { studentAssignmentDto.Id }, ct);
            if (studentAssignment == null) throw new KeyNotFoundException("Student Assignment not found");

            _mapper.Map(studentAssignmentDto, studentAssignment);
            studentAssignment.StudentId = _userContext.GetStudentId();
            _context.StudentAssignments.Update(studentAssignment);
            StudentActivityLogDto logDto = new StudentActivityLogDto
            {
                StudentId = studentAssignment.StudentId,
                ActivityType = "Updated Assignment",
                Description = $"Updated assignment with {studentAssignment.Assignment.Title}",
                CreatedAt = DateTime.UtcNow
            };
            var activitylog = _mapper.Map<StudentActivityLog>(logDto);
            await _context.StudentActivityLogs.AddAsync(activitylog);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<StudentAssignmentDto>(studentAssignment);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var studentAssignment = await _context.StudentAssignments.FindAsync(new object[] { id }, ct);
            if (studentAssignment != null)
            {
                StudentActivityLogDto logDto = new StudentActivityLogDto
                {
                    StudentId = studentAssignment.StudentId,
                    ActivityType = "Deleted Assignment",
                    Description = $"Deleted assignment with {studentAssignment.Assignment.Title}",
                    CreatedAt = DateTime.UtcNow
                };
                var activitylog = _mapper.Map<StudentActivityLog>(logDto);
                await _context.StudentActivityLogs.AddAsync(activitylog);
                _context.StudentAssignments.Remove(studentAssignment);
                await _context.SaveChangesAsync(ct);
            }
        }
    }

}
