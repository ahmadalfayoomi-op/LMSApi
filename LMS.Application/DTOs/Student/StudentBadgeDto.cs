

namespace LMS.Application.DTOs.Student
{
    public class StudentBadgeDto
    {
        public int Id { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int BadgeId { get; set; }
        public string BadgeName { get; set; } = string.Empty;
        public DateTime AwardedAt { get; set; }
    }
}
