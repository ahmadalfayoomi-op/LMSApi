using Microsoft.AspNetCore.Http;


namespace LMS.Application.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public IFormFile? ProfileImage { get; set; }
        public string? ProfileImagePath { get; set; } 
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public List<RoleDto> Roles { get; set; } = new();

        public List<string> StringRoles { get; set; } = new();

        // Flattened permissions
        public List<string> PermissionNames { get; set; } = new();
    }

}
