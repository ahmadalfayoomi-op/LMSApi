using LMS.Domain.Enums;

namespace LMS.Application.DTOs.Quizzes
{
    public class GenerateCourseQuizRequest
    {
        public int CourseId { get; set; }
        public string Type { get; set; } 
        public int QuestionsPerLesson { get; set; } = 5;
    }

}
