
namespace LMS.Domain.Entities
{
    public class StudentBadge 
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int BadgeId { get; set; }
        public Badge Badge { get; set; } = null!;
        public DateTime AwardedAt { get; set; } = DateTime.UtcNow;
    }
}
