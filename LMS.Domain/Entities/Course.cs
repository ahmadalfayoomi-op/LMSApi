using LMS.Domain.Common;


namespace LMS.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public int? InstructorId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Instructor? Instructor { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
