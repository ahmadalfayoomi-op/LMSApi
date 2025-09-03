using LMS.Domain.Common;

namespace LMS.Domain.Entities
{
    public class Instructor : BaseEntity
    {
        public int UserId { get; set; }
        public string Addres { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public string Headline { get; set; } = string.Empty;
        public User? User { get; set; }
        public ICollection<Course>? Courses { get; set; }

    }
}
