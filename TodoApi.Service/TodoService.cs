using TodoApi.Models;
using TodoApi.Data.Interfaces;
using TodoApi.Service.Interfaces;
using TodoApi.Model.Request.Todo;
using TodoApi.Util.Exceptions;

namespace TodoApi.Service
{
    public class TodoService(ITodoRepository todoRepository, IUserService userService) : ITodoService
    {
        public async Task Create(CreateTodoRequest todo)
        {
            var _ = await userService.GetById(todo.UserId) ?? throw new ResponseException("User not found");

            await todoRepository.Create(todo);
        }

        public async Task<List<Todo>> GetAll() => await todoRepository.GetAll();

        public async Task<List<Todo>> GetByUserId(int userId)
        {
            if (userId == 0)
                throw new ResponseException("User id not provided");

            return await todoRepository.GetByUserId(userId);
        }

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
