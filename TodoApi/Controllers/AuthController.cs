using Microsoft.AspNetCore.Mvc;
using System.Net;
using TodoApi.Config;
using TodoApi.Model.Request.Auth;
using TodoApi.Model.Response;
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
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAuthRequest request)
        {
            var response = await authService.Login(request);
            return ConfigureResponse.GenerateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Refresh the access token
        /// </summary>
        /// <param name="request">The request body</param>
        /// <returns>The LoginResponse with the refreshed access token</returns>
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var response = await authService.RefreshToken(request);

            return ConfigureResponse.GenerateResponse(HttpStatusCode.OK, response);
        }
    }
}
