
namespace LMS.Application.DTOs.Student
{
    public class DiscussionDto
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<DiscussionReplyDto> Replies { get; set; } = new();
    }
}
