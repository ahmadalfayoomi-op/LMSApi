

namespace LMS.Application.DTOs.Student
{
    public class StudentQuizAttemptDto
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public double Score { get; set; }
        public bool Passed { get; set; }

        public List<StudentAnswerDto> StudentAnswers { get; set; } = new();
    }
}
