using LMS.Application.Common;


namespace LMS.Domain.Entities
{
    public class CourseEvent : BaseEntity
    {
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Description { get; set; }
    }
}
