using System.Security.Claims;
using BC.Service.Exam.Services;

namespace BC.Service.Exam.Infrastructure
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            Guid.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
            UserId = userId;
            IsAuthenticated = UserId != default;
        }

        public Guid UserId { get; }

        public bool IsAuthenticated { get; }
    }
}
