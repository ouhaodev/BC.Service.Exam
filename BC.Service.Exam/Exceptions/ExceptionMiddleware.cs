using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace BC.Service.Exam.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        /// <summary>
        /// constructor
        /// </summary>
        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ResponseException e)
            {
                await SetResponse(context, (int)e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                await SetResponse(context, StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        private static async Task SetResponse(HttpContext context, int statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(message));

        }
    }
}
