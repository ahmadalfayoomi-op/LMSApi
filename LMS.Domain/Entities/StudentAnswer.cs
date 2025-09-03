using LMS.Domain.Common;


namespace LMS.Domain.Entities
{
    public class StudentAnswer : BaseEntity
    {
        public int AttemptId { get; set; }
        public StudentQuizAttempt Attempt { get; set; } = null!;

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        public int? AnswerId { get; set; }
        public string? FreeTextAnswer { get; set; } 
    }
}
