using Microsoft.AspNetCore.Authorization;
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
        [HttpPut("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var response = await authService.RefreshToken(request);
            return ConfigureResponse.GenerateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Revokes the authentication credentials for the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose credentials are to be revoked. Must be a valid user ID.</param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        [HttpDelete("revoke/{userId:int}")]
        public async Task<IActionResult> Revoke([FromRoute] int userId)
        {
            await authService.Revoke(userId);
            return ConfigureResponse.GenerateResponse(HttpStatusCode.NoContent, null);
        }

        /// <summary>
        /// Revokes all active authentication tokens for the current user.
        /// </summary>
        /// <remarks>Use this method to immediately invalidate all authentication tokens associated with
        /// the current user. Subsequent requests using previously issued tokens will be denied access. This action
        /// cannot be undone.</remarks>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        [HttpDelete("revoke")]
        public IActionResult RevokeAll()
        {
            authService.RevokeAll();
            return ConfigureResponse.GenerateResponse(HttpStatusCode.NoContent, null);
        }
    }
}
