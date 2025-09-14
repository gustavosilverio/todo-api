using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Net;
using TodoApi.Models.Response;

namespace TodoApi.Config
{
    public sealed class ProblemExceptionHandler(ILogger<ProblemExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            try
            {
                ResponseObject<object> response;
                int statusCode;

                switch (exception)
                {
                    case SqlException sqlException:
                        statusCode = StatusCodes.Status500InternalServerError;
                        response = new(HttpStatusCode.InternalServerError, null, "Intern server error.");
                        logger.LogError(sqlException, "SQL error: {Message}", sqlException.Message);
                        break;

                    default:
                        statusCode = StatusCodes.Status500InternalServerError;
                        const string errorMessage = "An unexpected error occurred while processing your request.";
                        response = new(HttpStatusCode.InternalServerError, null, errorMessage);
                        logger.LogCritical(exception, "Error not handled: {Path}", httpContext.Request.Path);
                        break;
                }

                httpContext.Response.StatusCode = statusCode;
                await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
