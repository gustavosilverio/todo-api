using Microsoft.AspNetCore.Mvc;
using System.Net;
using TodoApi.Models.Response;

namespace TodoApi.Config
{
    internal static class ConfigureResponse
    {
        /// <summary>
        /// Generate a response for the endpoints.
        /// </summary>
        /// <param name="code">The HTTP status code that will be returned.</param>
        /// <param name="body">The response body, if applicable.</param>
        /// <returns>Instance of <see cref="IActionResult"/> that represents the formated response.</returns>
        internal static IActionResult GenerateResponse(HttpStatusCode code, object? body = null)
        {
            if (code == HttpStatusCode.NoContent)
                return new NoContentResult();

            var response = new ResponseObject<object>(code, body, null);

            var result = new ObjectResult(response) { StatusCode = (int)code };

            return result;
        }
    }
}
