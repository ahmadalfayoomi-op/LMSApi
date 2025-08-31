
namespace LMS.Domain.Entities
{
    public class StudentActivityLog
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public string? ActivityType { get; set; } 
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
