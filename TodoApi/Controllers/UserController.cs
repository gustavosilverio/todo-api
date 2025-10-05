using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TodoApi.Config;
using TodoApi.Model.Request.User;
using TodoApi.Models;
using TodoApi.Service.Interfaces;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController(IUserService userService) : ControllerBase
    {

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user information to create. The request body must contain all required user fields. Cannot be null.</param>
        /// <returns>A response with status code 204 (No Content) if the user is created successfully; otherwise, a 400 (Bad
        /// Request) response if the input is invalid.</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest user)
        {
            await userService.Create(user);
            return ConfigureResponse.GenerateResponse(HttpStatusCode.NoContent, null);
        }

        /// <summary>
        /// Updates the user with the specified identifier.
        /// </summary>
        /// <remarks>This method requires a valid user ID and a complete update request. The user
        /// information is replaced with the data provided in <paramref name="request"/>. Ensure that all required
        /// fields are present in the request object.</remarks>
        /// <param name="id">The unique identifier of the user to update. Must be a valid user ID.</param>
        /// <param name="request">An object containing the updated user information. Cannot be null.</param>
        /// <returns>The updated user</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserRequest request)
        {
            request.Id = id;

            var updatedUser = await userService.Update(request);
            return ConfigureResponse.GenerateResponse(HttpStatusCode.OK, updatedUser);
        }

        /// <summary>
        /// Delete the user by the provided id
        /// </summary>
        /// <param name="id">The id of the user</param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await userService.Delete(id);
            return ConfigureResponse.GenerateResponse(HttpStatusCode.NoContent);
        }
        
        /// <summary>
        /// Get all the users
        /// </summary>
        /// <returns>A list of users</returns>
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var users = await userService.GetAll();
            return ConfigureResponse.GenerateResponse(HttpStatusCode.OK, users);
        }

        /// <summary>
        /// Retrieves the user with the specified unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> containing the user data if found; otherwise, a response with status code 204
        /// (No Content) if the user does not exist or 400 (Bad Request) if the request is invalid.</returns>
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await userService.GetById(id);
            return ConfigureResponse.GenerateResponse(HttpStatusCode.OK, user);
        }
    }
}
