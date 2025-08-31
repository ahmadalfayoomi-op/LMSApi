using AutoMapper;
using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Student;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LMS.Infrastructure.Repositories.Student
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;

        public BookmarkRepository(AppDbContext context, IMapper mapper, IUserContextService userContext)
        {
            _context = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IEnumerable<BookmarkDto>> GetAllByStudentAsync(CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var bookmarks = await _context.Bookmarks
                .Where(b => b.StudentId == studentId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<BookmarkDto>>(bookmarks);
        }

        public async Task<BookmarkDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var bookmark = await _context.Bookmarks.FindAsync(new object[] { id }, ct);
            return bookmark == null ? null : _mapper.Map<BookmarkDto>(bookmark);
        }

        public async Task<BookmarkDto> AddAsync(BookmarkDto bookmarkDto, CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var bookmark = _mapper.Map<Bookmark>(bookmarkDto);
            bookmark.StudentId= studentId;
            _context.Bookmarks.Add(bookmark);
            await _context.SaveChangesAsync(ct);

            StudentActivityLogDto logDto = new StudentActivityLogDto
            {
                StudentId = studentId,
                ActivityType = "AddBookmark",
                Description = $"Added bookmark for Lesson {bookmark.Lesson.Title}",
                CreatedAt = DateTime.UtcNow
            };

            var activitylog = _mapper.Map<StudentActivityLog>(logDto);
            await _context.StudentActivityLogs.AddAsync(activitylog);
            await _context.SaveChangesAsync(ct);

            return _mapper.Map<BookmarkDto>(bookmark);

        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var bookmark = await _context.Bookmarks.FindAsync(new object[] { id }, ct);
            if (bookmark != null)
            {
                StudentActivityLogDto logDto = new StudentActivityLogDto
                {
                    StudentId = bookmark.StudentId,
                    ActivityType = "RemoveBookmark",
                    Description = $"Remove bookmark for Lesson {bookmark.Lesson.Title}",
                    CreatedAt = DateTime.UtcNow
                };

                var activitylog = _mapper.Map<StudentActivityLog>(logDto);
                await _context.StudentActivityLogs.AddAsync(activitylog);
                _context.Bookmarks.Remove(bookmark);
                await _context.SaveChangesAsync(ct);
            }
        }
    }

}
