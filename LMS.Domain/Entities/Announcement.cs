using LMS.Application.Common;


namespace LMS.Domain.Entities
{
    public class Announcement : BaseEntity
    {
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    }
}
