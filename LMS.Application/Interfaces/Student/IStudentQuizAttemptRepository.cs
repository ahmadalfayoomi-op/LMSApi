using LMS.Application.DTOs.Student;

namespace LMS.Application.Interfaces.Student
{
    public interface IStudentQuizAttemptRepository
    {
        Task<StudentQuizAttemptDto> StartAttemptAsync(int quizId, CancellationToken ct = default);
        Task<StudentQuizAttemptDto?> GetAttemptAsync(int attemptId, CancellationToken ct = default);
        Task<StudentQuizAttemptDto> SubmitAttemptAsync(StudentQuizAttemptDto attemptDto, CancellationToken ct = default);
    }
}
