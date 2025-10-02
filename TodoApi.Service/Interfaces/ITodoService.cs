using TodoApi.Model.Request.Todo;
using TodoApi.Models;

namespace TodoApi.Service.Interfaces
{
    public interface ITodoService
    {
        Task Create(CreateTodoRequest todo);
        Task<List<Todo>> GetAll();
        Task<List<Todo>> GetByUserId(int userId);
        Task<Todo> GetById(int id);
        Task<Todo> UpdateIsDone(int idTodo, bool isDone);
        Task<Todo> Update(UpdateTodoRequest todo);
        Task Delete(int id);
    }
}
