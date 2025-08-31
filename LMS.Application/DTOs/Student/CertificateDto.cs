

namespace LMS.Application.DTOs.Student
{
    public class CertificateDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime IssuedAt { get; set; }
        public string CertificateUrl { get; set; } = string.Empty;
    }

}
