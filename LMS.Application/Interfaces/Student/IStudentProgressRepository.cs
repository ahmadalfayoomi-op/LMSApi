using LMS.Application.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Interfaces.Student
{
    public interface IStudentProgressRepository
    {
        Task<StudentProgressDto> MarkLessonCompletedAsync(int lessonId, CancellationToken ct = default);
        Task<IEnumerable<StudentProgressDto>> GetStudentProgressAsync(CancellationToken ct = default);
    }
}
