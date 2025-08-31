using LMS.Application.Interfaces.Configuration;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LMS.Infrastructure.Repositories.Configuration
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var claim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (claim == null || !int.TryParse(claim, out var userId))
                throw new UnauthorizedAccessException("UserId claim not found.");
            return userId;
        }

        public int GetStudentId()
        {
            var claim = _httpContextAccessor.HttpContext?.User?.FindFirst("studentId")?.Value;
            if (claim == null || !int.TryParse(claim, out var studentId))
                throw new UnauthorizedAccessException("StudentId claim not found.");
            return studentId;
        }
    }

}
