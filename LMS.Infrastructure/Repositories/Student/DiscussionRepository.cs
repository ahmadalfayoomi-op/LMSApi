using AutoMapper;
using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Student;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LMS.Infrastructure.Repositories.Student
{
    public class DiscussionRepository : IDiscussionRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;


        public DiscussionRepository(AppDbContext context, IMapper mapper, IUserContextService userContext)
        {
            _context = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IEnumerable<DiscussionDto>> GetAllByLessonAsync(int lessonId, CancellationToken ct = default)
        {
            var discussions = await _context.Discussions
                .Include(d => d.Replies)
                .Where(d => d.LessonId == lessonId)
                .OrderBy(d => d.Id)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<DiscussionDto>>(discussions);
        }

        public async Task<DiscussionDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var discussion = await _context.Discussions
                .Include(d => d.Replies)
                .FirstOrDefaultAsync(d => d.Id == id, ct);

            return discussion == null ? null : _mapper.Map<DiscussionDto>(discussion);
        }

        public async Task<DiscussionDto> AddDiscussionAsync(DiscussionDto discussionDto, CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var discussion = _mapper.Map<Discussion>(discussionDto);
            discussion.StudentId= studentId;
            _context.Discussions.Add(discussion);
            await _context.SaveChangesAsync(ct);

            StudentActivityLogDto logDto = new StudentActivityLogDto
            {
                StudentId = studentId,
                ActivityType = "AddDiscussion",
                Description = $"Added discussion for Lesson {discussion.Lesson.Title}",
                CreatedAt = DateTime.UtcNow
            };

            var activitylog = _mapper.Map<StudentActivityLog>(logDto);
            await _context.StudentActivityLogs.AddAsync(activitylog);
            await _context.SaveChangesAsync(ct);

            return _mapper.Map<DiscussionDto>(discussion);
        }

        public async Task<DiscussionReplyDto> AddReplyAsync(DiscussionReplyDto replyDto, CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var reply = _mapper.Map<DiscussionReply>(replyDto);
            reply.StudentId= studentId;
            _context.DiscussionReplies.Add(reply);
            await _context.SaveChangesAsync(ct);

            StudentActivityLogDto logDto = new StudentActivityLogDto
            {
                StudentId = studentId,
                ActivityType = "AddReply",
                Description = $"Added reply to Discussion {reply.Discussion.Message}",
                CreatedAt = DateTime.UtcNow
            };
            var activitylog = _mapper.Map<StudentActivityLog>(logDto);
            await _context.StudentActivityLogs.AddAsync(activitylog);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<DiscussionReplyDto>(reply);
        }

        public async Task DeleteDiscussionAsync(int id, CancellationToken ct = default)
        {
            var discussion = await _context.Discussions.FindAsync(new object[] { id }, ct);
            if (discussion != null)
            {
                StudentActivityLogDto logDto = new StudentActivityLogDto
                {
                    StudentId = discussion.StudentId,
                    ActivityType = "DeleteDiscussion",
                    Description = $"Deleted discussion for Lesson {discussion.Lesson.Title}",
                    CreatedAt = DateTime.UtcNow
                };
                var activitylog = _mapper.Map<StudentActivityLog>(logDto);
                await _context.StudentActivityLogs.AddAsync(activitylog);
                _context.Discussions.Remove(discussion);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task DeleteReplyAsync(int id, CancellationToken ct = default)
        {
            var reply = await _context.DiscussionReplies.FindAsync(new object[] { id }, ct);
            if (reply != null)
            {
                StudentActivityLogDto logDto = new StudentActivityLogDto
                {
                    StudentId = reply.StudentId,
                    ActivityType = "DeleteReply",
                    Description = $"Deleted reply to Discussion {reply.Discussion.Message}",
                    CreatedAt = DateTime.UtcNow
                };
                var activitylog = _mapper.Map<StudentActivityLog>(logDto);
                await _context.StudentActivityLogs.AddAsync(activitylog);
                _context.DiscussionReplies.Remove(reply);
                await _context.SaveChangesAsync(ct);
            }
        }
    }

}
