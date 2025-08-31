using LMS.Application.DTOs.Student;

namespace LMS.Application.DTOs.Course
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CourseId { get; set; }
        public virtual CourseDto Course { get; set; } = null!;
        public bool IsCompleted { get; set; } = false;
    }
}
