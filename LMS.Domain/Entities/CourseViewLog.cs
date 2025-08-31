
namespace LMS.Domain.Entities
{
    public class CourseViewLog
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int? CourseId { get; set; }
        public Course? Course { get; set; }

        public int? LessonId { get; set; }
        public Lesson? Lesson { get; set; }

        public DateTime ViewedAt { get; set; } = DateTime.UtcNow;
    }

}
