

namespace LMS.Application.DTOs.Course
{
    public class FavoriteCourseDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
