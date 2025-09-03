using LMS.Domain.Common;
using LMS.Domain.Enums;


namespace LMS.Domain.Entities
{
    public class Question : BaseEntity
    {
        public string Text { get; set; } = string.Empty;
        public QuestionType Type { get; set; } = QuestionType.MultipleChoice;

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } = null!;

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
