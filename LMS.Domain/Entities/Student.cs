
namespace LMS.Domain.Entities
{
    public class Student 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Language { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public string Headline { get; set; } = string.Empty;
        public User? User { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<StudentBadge> StudentBadges { get; set; } = new List<StudentBadge>();

    }
}
