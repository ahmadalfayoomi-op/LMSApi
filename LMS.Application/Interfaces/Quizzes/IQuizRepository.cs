using LMS.Application.DTOs.Quizzes;


namespace LMS.Application.Interfaces.Quizzes
{
    public interface IQuizRepository
    {
        Task<IEnumerable<QuizDto>> GetAllByCourseAsync(int courseId, CancellationToken ct = default);
        Task<QuizDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<QuizDto> AddAsync(QuizDto quizDto, CancellationToken ct = default);
        Task<QuizDto> UpdateAsync(QuizDto quizDto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }

}
