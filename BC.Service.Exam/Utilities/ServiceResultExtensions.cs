using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BC.Service.Exam.Utilities
{
    [ExcludeFromCodeCoverage]
    public static class ServiceResultExtensions
    {
        public static IActionResult ToActionResult<T>(this ServiceResult<T> serviceResult,
            ControllerBase controller, ProblemDetailsFactory problemDetailsFactory)
        {
            switch (serviceResult.StatusCode)
            {
                case StatusCodes.Status200OK:
                    {
                        return serviceResult.Content is not null ? controller.Ok(serviceResult.Content) : controller.Ok();
                    }
                case StatusCodes.Status204NoContent:
                    {
                        return controller.NoContent();
                    }
                case StatusCodes.Status400BadRequest:
                    {
                        var unsuccessfulServiceResult = serviceResult as UnsuccessfulServiceResult<T>;
                        var problemDetails = problemDetailsFactory.CreateProblemDetails(
                                controller.HttpContext,
                                StatusCodes.Status400BadRequest,
                                detail: unsuccessfulServiceResult?.ErrorMessage);
                        return controller.BadRequest(problemDetails);
                    }
                case StatusCodes.Status401Unauthorized:
                    {
                        var unsuccessfulServiceResult = serviceResult as UnsuccessfulServiceResult<T>;
                        return controller.Unauthorized(
                            problemDetailsFactory.CreateProblemDetails(
                                controller.HttpContext,
                                StatusCodes.Status401Unauthorized,
                                detail: unsuccessfulServiceResult?.ErrorMessage));
                    }
                case StatusCodes.Status404NotFound:
                    {
                        var unsuccessfulServiceResult = serviceResult as UnsuccessfulServiceResult<T>;
                        return controller.NotFound(
                            problemDetailsFactory.CreateProblemDetails(
                                controller.HttpContext,
                                StatusCodes.Status404NotFound,
                                detail: unsuccessfulServiceResult?.ErrorMessage));
                    }
                case StatusCodes.Status403Forbidden:
                case StatusCodes.Status503ServiceUnavailable:
                    {
                        var unsuccessfulServiceResult = serviceResult as UnsuccessfulServiceResult<T>;
                        return controller.StatusCode(serviceResult.StatusCode,
                            problemDetailsFactory.CreateProblemDetails(
                                controller.HttpContext,
                                serviceResult.StatusCode,
                                detail: unsuccessfulServiceResult?.ErrorMessage));
                    }
                default:
                    {
                        return controller.StatusCode(serviceResult.StatusCode, serviceResult.Content);
                    }
            }
        }
    }
}