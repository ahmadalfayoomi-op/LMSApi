using LMS.Domain.Common;


namespace LMS.Domain.Entities
{
    public class Certificate : BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public string CertificateUrl { get; set; } = string.Empty;
    }

}
