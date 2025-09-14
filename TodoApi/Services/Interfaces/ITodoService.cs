using TodoApi.Models;
using TodoApi.Models.Request;

namespace TodoApi.Services.Interfaces
{
    public interface ITodoService
    {
        Task Salvar(TodoRequest todo);
        Task<List<Todo>> GetAll();
    }
}
