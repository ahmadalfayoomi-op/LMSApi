

namespace LMS.Domain.Entities
{
    public class Enrollment 
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
        public int StudentId { get; set; }
        public virtual Student Student { get; set; } = null!;
        public bool IsCompleted { get; set; } = false;
    }
}
