
using LMS.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace LMS.Application.DTOs.Instructor
{
    public class InstructorDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Addres { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public string Headline { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public IFormFile? ProfileImage { get; set; }
        public string? ProfileImagePath { get; set; }
        public List<Role> Roles { get; set; } = new();
    }
}
