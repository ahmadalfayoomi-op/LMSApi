
namespace LMS.Domain.Entities
{
    public class DiscussionReply 
    {
        public int Id { get; set; }
        public int DiscussionId { get; set; }
        public Discussion Discussion { get; set; } = null!;
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
