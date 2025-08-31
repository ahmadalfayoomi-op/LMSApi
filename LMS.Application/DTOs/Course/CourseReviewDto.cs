

namespace LMS.Application.DTOs.Course
{
    public class CourseReviewDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

}
