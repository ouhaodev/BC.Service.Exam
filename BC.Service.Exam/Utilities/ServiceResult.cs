namespace BC.Service.Exam.Utilities
{
    public class ServiceResult<T>
    {
        public int StatusCode { get; init; }
        public T Content { get; init; }

        public ServiceResult(int statusCode, T content = default)
        {
            StatusCode = statusCode;
            Content = content;
        }
    }
}