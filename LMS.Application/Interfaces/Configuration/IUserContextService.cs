

namespace LMS.Application.Interfaces.Configuration
{
    public interface IUserContextService
    {
        int GetUserId();
        int GetStudentId();
        int GetInstructorId();
    }

}
