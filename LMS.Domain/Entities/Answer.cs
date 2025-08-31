using LMS.Application.Common;


namespace LMS.Domain.Entities
{
    public class Answer : BaseEntity
    {
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; } = false;

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}
