
namespace LMS.Application.DTOs.Quizzes
{
    public class GeneratedQuizDto
    {
        public string LessonTitle { get; set; } = string.Empty;
        public string QuizTitle { get; set; } = string.Empty;
        public List<QuestionDto> Questions { get; set; } = new();
    }
}
