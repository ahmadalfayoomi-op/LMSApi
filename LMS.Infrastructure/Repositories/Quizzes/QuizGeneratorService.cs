using LMS.Application.DTOs.Course;
using LMS.Application.DTOs.Quizzes;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Course;


namespace LMS.Infrastructure.Repositories.Configuration
{
    public class QuizGeneratorService : IQuizGeneratorService
    {
        private readonly IWhisperService _whisperService;

        public QuizGeneratorService(IWhisperService whisperService)
        {
            _whisperService = whisperService;
        }

        public async Task<GeneratedQuizDto> GenerateQuizFromLessonAsync(LessonDto lesson, int numberOfQuestions , string Type)
        {
            string transcription;

            // Transcribe audio/video lessons
            if (lesson.ContentUrl.EndsWith(".mp3") ||
                lesson.ContentUrl.EndsWith(".wav") ||
                lesson.ContentUrl.EndsWith(".mp4"))
            {
                transcription = await _whisperService.TranscribeAsync(lesson.ContentUrl);
            }
            else
            {
                // For text content
                transcription = await File.ReadAllTextAsync(lesson.ContentUrl);
            }

            var sentences = transcription.Split('.', StringSplitOptions.RemoveEmptyEntries);

            var quiz = new GeneratedQuizDto
            {
                LessonTitle = lesson.Title,
                QuizTitle = $"Quiz for {lesson.Title}"
            };

            for (int i = 0; i < Math.Min(numberOfQuestions, sentences.Length); i++)
            {
                var questionText = sentences[i].Trim();

                var answers = GenerateAnswersFromSentence(questionText);

                quiz.Questions.Add(new QuestionDto
                {
                    Text = questionText,
                    Type = Type,
                    Answers = answers
                });
            }

            return quiz;
        }

        public async Task<GeneratedQuizDto> GenerateQuizFromCourseAsync(CourseDto course, int questionsPerLesson , string Type)
        {
            var quiz = new GeneratedQuizDto
            {
                LessonTitle = course.Title,
                QuizTitle = $"Quiz for {course.Title}"
            };

            foreach (var lesson in course.Lessons)
            {
                var lessonQuiz = await GenerateQuizFromLessonAsync(lesson, questionsPerLesson, Type);

                // Add all lesson questions to the course quiz
                foreach (var q in lessonQuiz.Questions)
                {
                    quiz.Questions.Add(q);
                }
            }

            return quiz;
        }


        private List<AnswerDto> GenerateAnswersFromSentence(string sentence)
        {
            var words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                .Where(w => w.Length > 3)
                                .Take(4)
                                .ToList();

            var answers = new List<AnswerDto>();
            for (int i = 0; i < 4; i++)
            {
                answers.Add(new AnswerDto
                {
                    Text = i < words.Count ? words[i] : "Other option",
                    IsCorrect = i == 0
                });
            }

            return answers;
        }
    }
}
