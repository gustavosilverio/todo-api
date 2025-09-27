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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateTodoRequest todo)
        {
            await todoService.Create(todo);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Todo>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var todos = await todoService.GetAll();
            return Ok(todos);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Todo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{idTodo}")]
        [Authorize]
        public async Task<IActionResult> UpdateIsDone([FromRoute] int idTodo, [FromQuery] bool isDone)
        {
            var todo = await todoService.UpdateIsDone(idTodo, isDone);
            return Ok(todo);
        }

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