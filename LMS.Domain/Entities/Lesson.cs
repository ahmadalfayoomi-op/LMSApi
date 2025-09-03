using LMS.Domain.Common;


namespace LMS.Domain.Entities
{
    public class Lesson : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string ContentUrl { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}
