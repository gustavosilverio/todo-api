using TodoApi.Models;
using TodoApi.Models.Request;

namespace TodoApi.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        Task Salvar(TodoRequest todo);
        public Task<List<Todo>> GetAll();
    }
}
