using Microsoft.AspNetCore.Http;


namespace LMS.Application.DTOs.Course
{
    public class LessonDto
    {
        public int Id { get; set; }               
        public string Title { get; set; } = string.Empty;
        public string ContentUrl { get; set; } = string.Empty;
        public IFormFile? ContentUploaded { get; set; }
        public int CourseId { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
