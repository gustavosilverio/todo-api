using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Model.Request.Todo;
using TodoApi.Models;
using TodoApi.Services.Interfaces;

namespace TodoApi.Controllers
{

    [ApiController]
    [Route("todo")]
    public class TodoController(ITodoService todoService) : ControllerBase
    {
        /// <summary>
        /// Create a to-do
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateTodoRequest todo)
        {
            await todoService.Create(todo);
            return NoContent();
        }

        /// <summary>
        /// Get all to-dos
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Todo>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var todos = await todoService.GetAll();
            return Ok(todos);
        }

        /// <summary>
        /// Update the IsDone field
        /// </summary>
        /// <param name="idTodo">The id of the to-do</param>
        /// <param name="isDone">If it is done or not</param>
        /// <returns>The updated to-do</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Todo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{idTodo}")]
        [Authorize]
        public async Task<IActionResult> UpdateIsDone([FromRoute] int idTodo, [FromQuery] bool isDone)
        {
            var todo = await todoService.UpdateIsDone(idTodo, isDone);
            return Ok(todo);
        }

        /// <summary>
        /// Update the to-do
        /// </summary>
        /// <param name="idTodo">The id of the to-do</param>
        /// <param name="todo">The to-do object</param>
        /// <returns>The updated to-do</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Todo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{idTodo}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int idTodo, [FromBody] UpdateTodoRequest todo)
        {
            todo.Id = idTodo;

            var updatedTodo = await todoService.Update(todo);
            return Ok(updatedTodo);
        }

        /// <summary>
        /// Delete a to-do
        /// </summary>
        /// <param name="idTodo">The if of the to-do</param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Todo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{idTodo}")]
        public async Task<IActionResult> Delete([FromRoute] int idTodo)
        {
            await todoService.Delete(idTodo);
            return NoContent();
        }
    }
}