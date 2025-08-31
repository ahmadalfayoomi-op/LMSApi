

namespace LMS.Application.DTOs.User
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public List<int> PermissionIds { get; set; } = new();
        public List<string> PermissionNames { get; set; } = new();
    }
}
