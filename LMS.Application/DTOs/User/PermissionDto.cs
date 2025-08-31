
namespace LMS.Application.DTOs.User
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public ICollection<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }
}
