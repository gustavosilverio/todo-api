using TodoApi.Models;
using TodoApi.Models.Request;

namespace TodoApi.Data.Interfaces
{
    public interface ITodoRepository
    {
        Task Create(CreateTodoRequest todo);
        Task<List<Todo>> GetAll();
        Task<Todo> GetById(int id);
        Task UpdateIsDone(int idTodo, bool isDone);
        Task Update(UpdateTodoRequest todo);
        Task Delete(int id);
    }
}
