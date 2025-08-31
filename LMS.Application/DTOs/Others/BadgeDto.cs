namespace LMS.Application.DTOs.Others
{
    public class BadgeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
    }
}
