

namespace LMS.Application.DTOs.Student
{
    public class DiscussionReplyDto
    {
        public int Id { get; set; }
        public int DiscussionId { get; set; }
        public int StudentId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

}
