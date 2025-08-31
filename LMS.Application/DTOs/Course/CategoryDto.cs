
namespace LMS.Application.DTOs.Course
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<CourseDto> Courses { get; set; } = new List<CourseDto>();
    }
}
