using AutoMapper;
using LMS.Application.DTOs.Quizzes;
using LMS.Application.Interfaces.Quizzes;
using LMS.Domain.Entities;
using LMS.Domain.Enums;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LMS.Infrastructure.Repositories.Quizzes
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public QuizRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuizDto>> GetAllByCourseAsync(int courseId, CancellationToken ct = default)
        {
            var quizzes = await _context.Quizzes
                .Where(q => q.CourseId == courseId)
                .Include(q => q.Questions)
                    .ThenInclude(qn => qn.Answers)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<QuizDto>>(quizzes);
        }

        public async Task<QuizDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(qn => qn.Answers)
                .FirstOrDefaultAsync(q => q.Id == id, ct);

            return quiz == null ? null : _mapper.Map<QuizDto>(quiz);
        }

        public async Task<QuizDto> AddAsync(QuizDto quizDto, CancellationToken ct = default)
        {
            var quiz = _mapper.Map<Quiz>(quizDto);

            foreach (var question in quiz.Questions)
            {
                question.Quiz = quiz;
                foreach (var answer in question.Answers)
                {
                    answer.Question = question;
                }
            }

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<QuizDto>(quiz);
        }

        public async Task<QuizDto> UpdateAsync(QuizDto quizDto, CancellationToken ct = default)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(qn => qn.Answers)
                .FirstOrDefaultAsync(q => q.Id == quizDto.Id, ct);

            if (quiz == null)
                throw new KeyNotFoundException("Quiz not found");

            quiz.Title = quizDto.Title;
            quiz.Description = quizDto.Description;
            quiz.Duration = quizDto.Duration;
            quiz.IsPublished = quizDto.IsPublished;


            var questionIds = quizDto.Questions.Select(q => q.Id).ToList();
            var questionsToRemove = quiz.Questions.Where(q => !questionIds.Contains(q.Id)).ToList();
            _context.Questions.RemoveRange(questionsToRemove);

            foreach (var questionDto in quizDto.Questions)
            {
                var question = quiz.Questions.FirstOrDefault(q => q.Id == questionDto.Id);
                if (question == null)
                {
                    question = _mapper.Map<Question>(questionDto);
                    question.Quiz = quiz;
                    foreach (var answer in question.Answers)
                        answer.Question = question;
                    quiz.Questions.Add(question);
                }
                else
                {
                    question.Text = questionDto.Text;
                    question.Type = Enum.Parse<QuestionType>(questionDto.Type);

                    var answerIds = questionDto.Answers.Select(a => a.Id).ToList();
                    var answersToRemove = question.Answers.Where(a => !answerIds.Contains(a.Id)).ToList();
                    _context.Answers.RemoveRange(answersToRemove);

                    foreach (var answerDto in questionDto.Answers)
                    {
                        var answer = question.Answers.FirstOrDefault(a => a.Id == answerDto.Id);
                        if (answer == null)
                        {
                            answer = _mapper.Map<Answer>(answerDto);
                            answer.Question = question;
                            question.Answers.Add(answer);
                        }
                        else
                        {
                            answer.Text = answerDto.Text;
                            answer.IsCorrect = answerDto.IsCorrect;
                        }
                    }
                }
            }

            await _context.SaveChangesAsync(ct);
            return _mapper.Map<QuizDto>(quiz);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var quiz = await _context.Quizzes.FindAsync(new object[] { id }, ct);
            if (quiz != null)
            {
                _context.Quizzes.Remove(quiz);
                await _context.SaveChangesAsync(ct);
            }
        }
    }


}
