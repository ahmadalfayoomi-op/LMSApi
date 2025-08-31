using LMS.Application.DTOs.Course;
using LMS.Domain.Entities;
using Microsoft.AspNetCore.Http;


namespace LMS.Application.DTOs.Student
{
    public class StudentDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public IFormFile? ProfileImage { get; set; }
        public string? ProfileImagePath { get; set; }
        public List<Role> Roles { get; set; } = new();
        public string Language { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public string Headline { get; set; } = string.Empty;
        public int UserId { get; set; }
        public ICollection<EnrollmentDto>? Enrollments { get; set; }
    }
}
