
namespace LMS.Application.DTOs.Student
{
    public class StudentActivityLogDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string? ActivityType { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
