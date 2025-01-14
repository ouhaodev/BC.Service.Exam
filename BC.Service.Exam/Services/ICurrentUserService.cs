namespace BC.Service.Exam.Services
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }

        bool IsAuthenticated { get; }
    }
}