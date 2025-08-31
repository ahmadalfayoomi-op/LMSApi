

namespace LMS.Domain.Entities
{
    public class StudentAssignment 
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = null!;
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public string? SubmissionUrl { get; set; }
        public double? Grade { get; set; }
        public bool IsGraded { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
