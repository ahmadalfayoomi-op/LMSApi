

namespace LMS.Application.DTOs.Course
{
    public class CourseViewLogDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public DateTime ViewedAt { get; set; }
        public int? LessonId { get; set; } 
    }
}
