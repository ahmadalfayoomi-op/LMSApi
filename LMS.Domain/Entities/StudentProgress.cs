
namespace LMS.Domain.Entities
{
    public class StudentProgress
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public Student? Student { get; set; }
        public Lesson? Lesson { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
