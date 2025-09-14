using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Models.Request;
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
        public async Task<IActionResult> Save(TodoRequest todo)
        {
            await todoService.Salvar(todo);
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Todo>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var todos = await todoService.GetAll();
            return Ok(todos);
        }
    }
}
