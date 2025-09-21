using TodoApi.Models;
using TodoApi.Models.Request;

namespace TodoApi.Services.Interfaces
{
    public interface ITodoService
    {
        Task Create(CreateTodoRequest todo);
        Task<List<Todo>> GetAll();
        Task<Todo> GetById(int id);
        Task<Todo> UpdateIsDone(int idTodo, bool isDone);
        Task<Todo> Update(UpdateTodoRequest todo);
        Task Delete(int id);
    }
}
