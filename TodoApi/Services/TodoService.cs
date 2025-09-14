using TodoApi.Models;
using TodoApi.Models.Request;
using TodoApi.Repositories.Interfaces;
using TodoApi.Services.Interfaces;

namespace TodoApi.Services
{
    public class TodoService(ITodoRepository todoRepository) : ITodoService
    {
        public async Task Salvar(TodoRequest todo) => await todoRepository.Salvar(todo);

        public async Task<List<Todo>> GetAll() => await todoRepository.GetAll();
    }
}
