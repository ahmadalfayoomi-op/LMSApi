using LMS.Domain.Common;

namespace LMS.Domain.Entities
{
    public class Badge : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public ICollection<StudentBadge> StudentBadges { get; set; } = new List<StudentBadge>();

    }
}
