using Microsoft.AspNetCore.Mvc;
using System.Net;
using TodoApi.Config;
using TodoApi.Model.Request.Auth;
using TodoApi.Service.Interfaces;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController(IAuthService authService)
    {
        /// <summary>
        /// Log the user
        /// </summary>
        /// <param name="request">The login request params</param>
        /// <returns>The token if user params are valid</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAuthRequest request)
        {
            var response = await authService.Login(request);
            return ConfigureResponse.GenerateResponse(HttpStatusCode.OK, response);
        }
    }
}
