using LMS.Domain.Common;


namespace LMS.Domain.Entities
{
    public class Quiz : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public TimeSpan? Duration { get; set; } 
        public bool IsPublished { get; set; } = false;

        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }

}
