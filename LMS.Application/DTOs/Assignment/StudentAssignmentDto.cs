
namespace LMS.Application.DTOs.Assignment
{
    public class StudentAssignmentDto
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public string? SubmissionUrl { get; set; }
        public double? Grade { get; set; }
        public bool IsGraded { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
