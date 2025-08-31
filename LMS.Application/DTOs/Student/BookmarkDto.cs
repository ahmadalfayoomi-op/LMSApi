
namespace LMS.Application.DTOs.Student
{
    public class BookmarkDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
