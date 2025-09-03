

using LMS.Domain.Enums;

namespace LMS.Application.DTOs.Quizzes
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string? Type { get; set; } = "MultipleChoice";

        public List<AnswerDto> Answers { get; set; } = new();
    }
}
