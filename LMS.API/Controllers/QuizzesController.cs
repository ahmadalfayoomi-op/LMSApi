using LMS.Application.DTOs.Quizzes;
using LMS.Application.Interfaces;
using LMS.Application.Interfaces.Course;
using LMS.Application.Interfaces.Quizzes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IQuizGeneratorService _quizGenerator;
        private readonly ICourseRepository _repository;

        public QuizzesController(IQuizRepository quizRepository, IQuizGeneratorService quizGenerator, ICourseRepository repository)
        {
            _quizRepository = quizRepository;
            _quizGenerator = quizGenerator;
            _repository = repository;
        }

        // ==========================
        // Existing CRUD Endpoints
        // ==========================

        [HttpGet("course/{courseId}")]
        [Authorize(Policy = "Quizzes.View")]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetByCourse(int courseId, CancellationToken ct)
        {
            var quizzes = await _quizRepository.GetAllByCourseAsync(courseId, ct);
            return Ok(quizzes);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Quizzes.View")]
        public async Task<ActionResult<QuizDto>> GetById(int id, CancellationToken ct)
        {
            var quiz = await _quizRepository.GetByIdAsync(id, ct);
            if (quiz == null) return NotFound();
            return Ok(quiz);
        }

        [HttpPost]
        [Authorize(Policy = "Quizzes.Create")]
        public async Task<ActionResult<QuizDto>> Create(QuizDto quizDto, CancellationToken ct)
        {
            var created = await _quizRepository.AddAsync(quizDto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Quizzes.Update")]
        public async Task<ActionResult<QuizDto>> Update(int id, QuizDto quizDto, CancellationToken ct)
        {
            if (id != quizDto.Id) return BadRequest("ID mismatch");
            var updated = await _quizRepository.UpdateAsync(quizDto, ct);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Quizzes.Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _quizRepository.DeleteAsync(id, ct);
            return NoContent();
        }

        // ==========================
        // New Endpoint: Generate Quiz for Entire Course
        // ==========================

        [HttpPost("generate-course-quiz")]
        [Authorize(Policy = "Quizzes.Create")]
        public async Task<IActionResult> GenerateCourseQuiz([FromBody] GenerateCourseQuizRequest request, CancellationToken ct)
        {
            // Fetch course and lessons from repository
            var course = await _repository.GetByIdAsync(request.CourseId, ct);
            if (course == null) return NotFound("Course not found");

            // Generate quiz using QuizGeneratorService
            var generatedQuiz = await _quizGenerator.GenerateQuizFromCourseAsync(course, request.QuestionsPerLesson , request.Type);

            // Save generated quiz to database
            var quizDto = new QuizDto
            {
                Title = generatedQuiz.QuizTitle,
                CourseId = course.Id,
                IsPublished = false,
                Questions = generatedQuiz.Questions.Select(q => new QuestionDto
                {
                    Text = q.Text,
                    Type = q.Type,
                    Answers = q.Answers.Select(a => new AnswerDto
                    {
                        Text = a.Text,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList()
            };

            var createdQuiz = await _quizRepository.AddAsync(quizDto, ct);

            return Ok(createdQuiz);
        }
    }


}
