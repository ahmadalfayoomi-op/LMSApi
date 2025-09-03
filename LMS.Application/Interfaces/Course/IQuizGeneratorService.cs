using LMS.Application.DTOs.Course;
using LMS.Application.DTOs.Quizzes;

namespace LMS.Application.Interfaces.Course
{
    public interface IQuizGeneratorService
    {
        Task<GeneratedQuizDto> GenerateQuizFromLessonAsync(LessonDto lesson, int numberOfQuestions , string Type);
        Task<GeneratedQuizDto> GenerateQuizFromCourseAsync(CourseDto course, int questionsPerLesson , string Type);
    }

}
