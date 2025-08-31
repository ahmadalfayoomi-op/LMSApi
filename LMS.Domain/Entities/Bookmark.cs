

namespace LMS.Domain.Entities
{
    public class Bookmark
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
