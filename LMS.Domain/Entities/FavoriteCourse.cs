
namespace LMS.Domain.Entities
{
    public class FavoriteCourse
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
