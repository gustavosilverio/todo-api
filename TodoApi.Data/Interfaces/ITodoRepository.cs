using TodoApi.Model.Request.Todo;
using TodoApi.Models;

namespace TodoApi.Data.Interfaces
{
    public interface ITodoRepository
    {
        Task Create(CreateTodoRequest todo);
        Task<List<Todo>> GetAll();
        Task<List<Todo>> GetByUserId(int userId);
        Task<Todo> GetById(int id);
        Task UpdateIsDone(int idTodo, bool isDone);
        Task Update(UpdateTodoRequest todo);
        Task Delete(int id);
    }
}
