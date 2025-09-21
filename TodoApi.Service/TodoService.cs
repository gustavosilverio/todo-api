using TodoApi.Models;
using TodoApi.Models.Request;
using TodoApi.Data.Interfaces;
using TodoApi.Services.Interfaces;

namespace TodoApi.Services
{
    public class TodoService(ITodoRepository todoRepository) : ITodoService
    {
        public async Task Create(CreateTodoRequest todo) => await todoRepository.Create(todo);

        public async Task<List<Todo>> GetAll() => await todoRepository.GetAll();

        public async Task<Todo> GetById(int id) => await todoRepository.GetById(id);

        public async Task<Todo> UpdateIsDone(int idTodo, bool isDone)
        {
            await todoRepository.UpdateIsDone(idTodo, isDone);

            return await todoRepository.GetById(idTodo);
        }

        public async Task<Todo> Update(UpdateTodoRequest todo)
        {
            await todoRepository.Update(todo);

            return await todoRepository.GetById(todo.Id);
        }

        public async Task Delete(int id) => await todoRepository.Delete(id);
    }
}
