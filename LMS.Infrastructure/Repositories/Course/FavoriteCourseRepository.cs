
using AutoMapper;
using LMS.Application.DTOs.Course;
using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Course;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories.Course
{
    public class FavoriteCourseRepository : IFavoriteCourseRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;


        public FavoriteCourseRepository(AppDbContext context, IMapper mapper, IUserContextService userContext)
        {
            _context = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IEnumerable<FavoriteCourseDto>> GetByStudentAsync(CancellationToken ct = default)
        {
            int studentId = _userContext.GetUserId();
            var favorites = await _context.FavoriteCourses
                .Where(f => f.StudentId == studentId)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<FavoriteCourseDto>>(favorites);
        }

        public async Task<FavoriteCourseDto> AddAsync(FavoriteCourseDto favoriteDto, CancellationToken ct = default)
        {
            var entity = _mapper.Map<LMS.Domain.Entities.FavoriteCourse>(favoriteDto);
            _context.FavoriteCourses.Add(entity);
            await _context.SaveChangesAsync(ct);
            StudentActivityLogDto log = new StudentActivityLogDto
            {
                StudentId = entity.StudentId,
                ActivityType = "AddFavorite",
                Description = $"Added course {entity.Course?.Title} to favorites.",
                CreatedAt = DateTime.UtcNow
            };
            await _context.StudentActivityLogs.AddAsync(_mapper.Map<LMS.Domain.Entities.StudentActivityLog>(log), ct);


            CourseViewLogDto viewLog = new CourseViewLogDto
            {
                StudentId = entity.StudentId,
                CourseId = entity.CourseId,
                ViewedAt = DateTime.UtcNow
            };
            await _context.CourseViewLogs.AddAsync(_mapper.Map<LMS.Domain.Entities.CourseViewLog>(viewLog), ct);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<FavoriteCourseDto>(entity);
        }

        public async Task RemoveAsync(int courseId, CancellationToken ct = default)
        {
                        
            int studentId = _userContext.GetUserId();
            var favorite = await _context.FavoriteCourses
                .FirstOrDefaultAsync(f => f.StudentId == studentId && f.CourseId == courseId, ct);

            if (favorite != null)
            {
                StudentActivityLogDto log = new StudentActivityLogDto
                {
                    StudentId = favorite.StudentId,
                    ActivityType = "RemoveFavorite",
                    Description = $"Removed course {favorite.Course?.Title} from favorites.",
                    CreatedAt = DateTime.UtcNow
                };
                await _context.StudentActivityLogs.AddAsync(_mapper.Map<LMS.Domain.Entities.StudentActivityLog>(log), ct);
                _context.FavoriteCourses.Remove(favorite);
                await _context.SaveChangesAsync(ct);
            }
        }
    }

}
