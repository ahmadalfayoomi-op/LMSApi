

namespace LMS.Domain.Entities
{
    public class CourseReview 
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow; 
    }
}
