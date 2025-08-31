using AutoMapper;
using LMS.Application.DTOs.Course;
using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Course;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories.Course
{
    public class CourseReviewRepository : ICourseReviewRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;


        public CourseReviewRepository(AppDbContext context, IMapper mapper, IUserContextService userContext)
        {
            _context = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IEnumerable<CourseReviewDto>> GetAllByCourseAsync(int courseId, CancellationToken ct = default)
        {
            var reviews = await _context.CourseReviews
                .Where(r => r.CourseId == courseId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<CourseReviewDto>>(reviews);
        }

        public async Task<CourseReviewDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var review = await _context.CourseReviews.FindAsync(new object[] { id }, ct);
            return review == null ? null : _mapper.Map<CourseReviewDto>(review);
        }

        public async Task<CourseReviewDto> AddAsync(CourseReviewDto reviewDto, CancellationToken ct = default)
        {
            var review = _mapper.Map<CourseReview>(reviewDto);
            review.CreatedAt = DateTime.UtcNow;
            review.StudentId = _userContext.GetStudentId();
            _context.CourseReviews.Add(review);
            await _context.SaveChangesAsync(ct);

            StudentActivityLogDto log = new StudentActivityLogDto
            {
                StudentId = review.StudentId,
                ActivityType = "AddReview",
                Description = $"Added review for course {review.Course?.Title}.",
                CreatedAt = DateTime.UtcNow
            };
            _context.StudentActivityLogs.Add(_mapper.Map<StudentActivityLog>(log));
            CourseViewLogDto viewLog = new CourseViewLogDto
            {
                StudentId = review.StudentId,
                CourseId = review.CourseId,
                ViewedAt = DateTime.UtcNow
            };
            _context.CourseViewLogs.Add(_mapper.Map<CourseViewLog>(viewLog));
            await _context.SaveChangesAsync(ct);

            return _mapper.Map<CourseReviewDto>(review);
        }

        public async Task<CourseReviewDto> UpdateAsync(CourseReviewDto reviewDto, CancellationToken ct = default)
        {
            var review = await _context.CourseReviews.FindAsync(new object[] { reviewDto.Id }, ct);
            if (review == null) throw new KeyNotFoundException("Review not found");

            _mapper.Map(reviewDto, review);
            review.StudentId = _userContext.GetStudentId();

            _context.CourseReviews.Update(review);
            StudentActivityLogDto log = new StudentActivityLogDto
            {
                StudentId = review.StudentId,
                ActivityType = "UpdateReview",
                Description = $"Updated review for course {review.Course?.Title}.",
                CreatedAt = DateTime.UtcNow
            };
            _context.StudentActivityLogs.Add(_mapper.Map<StudentActivityLog>(log));

            CourseViewLogDto viewLog = new CourseViewLogDto
            {
                StudentId = review.StudentId,
                CourseId = review.CourseId,
                ViewedAt = DateTime.UtcNow
            };
            _context.CourseViewLogs.Add(_mapper.Map<CourseViewLog>(viewLog));
            await _context.SaveChangesAsync(ct);

            return _mapper.Map<CourseReviewDto>(review);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var review = await _context.CourseReviews.FindAsync(new object[] { id }, ct);
            if (review != null)
            {
                _context.CourseReviews.Remove(review);
                await _context.SaveChangesAsync(ct);
            }
        }
    }

}
