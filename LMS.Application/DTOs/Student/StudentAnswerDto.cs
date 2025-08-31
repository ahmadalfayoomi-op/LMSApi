

namespace LMS.Application.DTOs.Student
{
    public class StudentAnswerDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public string? FreeTextAnswer { get; set; }
    }

}
