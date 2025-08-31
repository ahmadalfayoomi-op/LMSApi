using LMS.Application.DTOs.Student;


namespace LMS.Application.Interfaces.Student
{
    public interface IDiscussionRepository
    {
        Task<IEnumerable<DiscussionDto>> GetAllByLessonAsync(int lessonId, CancellationToken ct = default);
        Task<DiscussionDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<DiscussionDto> AddDiscussionAsync(DiscussionDto discussionDto, CancellationToken ct = default);
        Task<DiscussionReplyDto> AddReplyAsync(DiscussionReplyDto replyDto, CancellationToken ct = default);
        Task DeleteDiscussionAsync(int id, CancellationToken ct = default);
        Task DeleteReplyAsync(int id, CancellationToken ct = default);
    }
}
