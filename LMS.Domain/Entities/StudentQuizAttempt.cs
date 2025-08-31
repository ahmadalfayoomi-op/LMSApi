using LMS.Application.Common;


namespace LMS.Domain.Entities
{
    public class StudentQuizAttempt : BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } = null!;

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? FinishedAt { get; set; }
        public double Score { get; set; }
        public bool Passed { get; set; }

        public ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
    }
}
