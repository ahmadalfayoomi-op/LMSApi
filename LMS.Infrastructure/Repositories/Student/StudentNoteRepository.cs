using AutoMapper;
using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Student;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories.Student
{

    public class StudentNoteRepository : IStudentNoteRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;


        public StudentNoteRepository(AppDbContext db, IMapper mapper, IUserContextService userContext)
        {
            _db = db;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<StudentNoteDto> CreateAsync(StudentNoteDto dto, CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var entity = _mapper.Map<StudentNote>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.StudentId = studentId;
            _db.StudentNotes.Add(entity);
            await _db.SaveChangesAsync(ct);

            StudentActivityLogDto logDto = new StudentActivityLogDto
            {
                StudentId = studentId,
                ActivityType = "AddNote",
                Description = $"Added note for student {entity.Student?.User?.FirstName + " " + entity.Student?.User?.LastName}",
                CreatedAt = DateTime.UtcNow
            };
            var activitylog = _mapper.Map<StudentActivityLog>(logDto);
            await _db.StudentActivityLogs.AddAsync(activitylog);
            return _mapper.Map<StudentNoteDto>(entity);
        }

        public async Task<StudentNoteDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _db.StudentNotes.FindAsync(new object[] { id }, ct);
            return entity == null ? null : _mapper.Map<StudentNoteDto>(entity);
        }

        public async Task<IEnumerable<StudentNoteDto>> GetStudentNotesAsync(CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var notes = await _db.StudentNotes
                .Where(n => n.StudentId == studentId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<StudentNoteDto>>(notes);
        }

        public async Task<StudentNoteDto> UpdateAsync(StudentNoteDto dto, CancellationToken ct = default)
        {
            var entity = await _db.StudentNotes.FindAsync(new object[] { dto.Id }, ct);
            if (entity == null) throw new KeyNotFoundException("Note not found");
            int studentId = _userContext.GetStudentId();

            _mapper.Map(dto, entity);
            entity.UpdatedAt = DateTime.UtcNow;
            entity.StudentId = studentId;

            StudentActivityLogDto logDto = new StudentActivityLogDto
            {
                StudentId = studentId,
                ActivityType = "UpdateNote",
                Description = $"Updated note for student {entity.Student?.User?.FirstName + " " + entity.Student?.User?.LastName}",
                CreatedAt = DateTime.UtcNow
            };
            var activitylog = _mapper.Map<StudentActivityLog>(logDto);
            await _db.StudentActivityLogs.AddAsync(activitylog);
            _db.StudentNotes.Update(entity);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<StudentNoteDto>(entity);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _db.StudentNotes.FindAsync(new object[] { id }, ct);
            if (entity != null)
            {
                StudentActivityLogDto logDto = new StudentActivityLogDto
                {
                    StudentId = entity.StudentId,
                    ActivityType = "DeleteNote",
                    Description = $"Deleted note for student {entity.Student?.User?.FirstName + " " + entity.Student?.User?.LastName}",
                    CreatedAt = DateTime.UtcNow
                };
                var activitylog = _mapper.Map<StudentActivityLog>(logDto);
                await _db.StudentActivityLogs.AddAsync(activitylog);
                _db.StudentNotes.Remove(entity);
                await _db.SaveChangesAsync(ct);
            }
        }
    }

}
