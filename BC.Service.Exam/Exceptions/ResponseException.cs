using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace BC.Service.Exam.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class ResponseException : Exception
    {
        public HttpStatusCode StatusCode;
        public ResponseException(HttpStatusCode statusCode, string? message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
