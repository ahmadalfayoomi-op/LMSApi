using LMS.Domain.Common;


namespace LMS.Domain.Entities
{
    public class Discussion : BaseEntity
    {
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; } = null!;
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public string Message { get; set; } = string.Empty;
        public ICollection<DiscussionReply> Replies { get; set; } = new List<DiscussionReply>();

    }
}
