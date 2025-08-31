using AutoMapper;
using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Student;
using LMS.Domain.Entities;
using LMS.Domain.Enums;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LMS.Infrastructure.Repositories.Student
{
    public class StudentQuizAttemptRepository : IStudentQuizAttemptRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;

        public StudentQuizAttemptRepository(AppDbContext context, IMapper mapper, IUserContextService userContext)
        {
            _context = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<StudentQuizAttemptDto> StartAttemptAsync(int quizId, CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var attempt = new StudentQuizAttempt
            {
                StudentId = studentId,
                QuizId = quizId,
                StartedAt = DateTime.UtcNow
            };

            var quiz = await _context.Quizzes.Include(q => q.Questions).FirstOrDefaultAsync(q => q.Id == quizId, ct);

            StudentActivityLogDto logDto = new StudentActivityLogDto
            {
                StudentId = studentId,
                ActivityType = "StartQuizAttempt",
                Description = $"Started quiz attempt for Quiz {quiz?.Title}",
                CreatedAt = DateTime.UtcNow
            };
            var activitylog = _mapper.Map<StudentActivityLog>(logDto);
            await _context.StudentActivityLogs.AddAsync(activitylog);
            _context.StudentQuizAttempts.Add(attempt);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<StudentQuizAttemptDto>(attempt);
        }

        public async Task<StudentQuizAttemptDto?> GetAttemptAsync(int attemptId, CancellationToken ct = default)
        {
            var attempt = await _context.StudentQuizAttempts
                .Include(a => a.StudentAnswers)
                .FirstOrDefaultAsync(a => a.Id == attemptId, ct);

            return attempt == null ? null : _mapper.Map<StudentQuizAttemptDto>(attempt);
        }

        public async Task<StudentQuizAttemptDto> SubmitAttemptAsync(StudentQuizAttemptDto attemptDto, CancellationToken ct = default)
        {

            // Load attempt with existing answers and quiz questions
            var attempt = await _context.StudentQuizAttempts
                .Include(a => a.StudentAnswers)
                .Include(a => a.Quiz)
                    .ThenInclude(q => q.Questions)
                        .ThenInclude(qn => qn.Answers)
                .FirstOrDefaultAsync(a => a.Id == attemptDto.Id, ct);

            if (attempt == null)
                throw new KeyNotFoundException("Attempt not found");

            attempt.FinishedAt = DateTime.UtcNow;

            // -----------------------------
            // Add or update student answers
            // -----------------------------
            foreach (var answerDto in attemptDto.StudentAnswers)
            {
                var existingAnswer = attempt.StudentAnswers.FirstOrDefault(a => a.Id == answerDto.Id);

                if (existingAnswer == null)
                {
                    // New answer
                    var newAnswer = _mapper.Map<StudentAnswer>(answerDto);
                    newAnswer.AttemptId = attempt.Id;      // set FK
                    newAnswer.QuestionId = answerDto.QuestionId;
                    _context.StudentAnswers.Add(newAnswer); // mark as Added
                    attempt.StudentAnswers.Add(newAnswer);  // optional for navigation
                }
                else
                {
                    // Update existing answer
                    existingAnswer.AnswerId = answerDto.AnswerId;
                    existingAnswer.FreeTextAnswer = answerDto.FreeTextAnswer;
                }
            }

            // -----------------------------
            // Calculate score & pass/fail
            // -----------------------------
            int totalQuestions = attempt.Quiz.Questions.Count;

            if (totalQuestions == 0)
            {
                attempt.Score = 0;
                attempt.Passed = false;
            }
            else
            {
                double correctCount = 0;

                foreach (var question in attempt.Quiz.Questions)
                {
                    var studentAnswer = attempt.StudentAnswers.FirstOrDefault(a => a.QuestionId == question.Id);
                    if (studentAnswer == null) continue;

                    switch (question.Type)
                    {
                        case QuestionType.MultipleChoice:
                        case QuestionType.TrueFalse:
                            if (studentAnswer.AnswerId.HasValue)
                            {
                                var correctAnswer = question.Answers
                                    .FirstOrDefault(a => a.IsCorrect && a.Id == studentAnswer.AnswerId);
                                if (correctAnswer != null) correctCount++;
                            }
                            break;

                        case QuestionType.ShortAnswer:
                            if (!string.IsNullOrWhiteSpace(studentAnswer.FreeTextAnswer))
                                correctCount++;
                            break;
                    }
                }


            }

            StudentActivityLogDto logDto = new StudentActivityLogDto
            {
                StudentId = attempt.StudentId,
                ActivityType = "SubmitQuizAttempt",
                Description = $"Submitted quiz attempt for Quiz {attempt.Quiz?.Title}",
                CreatedAt = DateTime.UtcNow
            };
            var activitylog = _mapper.Map<StudentActivityLog>(logDto);
            await _context.StudentActivityLogs.AddAsync(activitylog);
            await _context.SaveChangesAsync(ct);

            return _mapper.Map<StudentQuizAttemptDto>(attempt);
        }

    }

}
