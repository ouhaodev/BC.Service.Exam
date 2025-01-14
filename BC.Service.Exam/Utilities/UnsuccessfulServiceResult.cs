namespace BC.Service.Exam.Utilities
{
    public class UnsuccessfulServiceResult<T> : ServiceResult<T>
    {
        public string ErrorMessage { get; init; }

        public UnsuccessfulServiceResult(int statusCode, string errorMessage) : base(statusCode)
        {
            ErrorMessage = errorMessage;
        }
    }
}