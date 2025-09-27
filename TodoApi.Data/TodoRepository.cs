using Microsoft.Extensions.Configuration;
using PetaPoco.SqlKata;
using SqlKata;
using TodoApi.Models;
using TodoApi.Data.Interfaces;
using TodoApi.Model.Request.Todo;

namespace TodoApi.Data
{
    public class TodoRepository(IConfiguration config) : BaseRepository(config), ITodoRepository
    {
        public async Task Create(CreateTodoRequest todo)
        {
            await db.InsertAsync("Todo", todo);
        }

        public async Task<List<Todo>> GetAll()
        {
            var query = new Query("Todo").ToSql();

            return await db.FetchAsync<Todo>(query);
        }

        public async Task<Todo> GetById(int id)
        {
            var query = new Query("Todo")
                .Where("Id", id).ToSql();

            return await db.FirstOrDefaultAsync<Todo>(query);
        }
        public async Task UpdateIsDone(int id, bool isDone)
        {
            var query = new Query("Todo")
                .Where("Id", id)
                .AsUpdate(new
                {
                    IsDone = isDone,
                }).ToSql();

            await db.ExecuteAsync(query);
        }

        public async Task Update(UpdateTodoRequest todo)
        {
            var query = new Query("Todo")
                .Where("Id", todo.Id)
                .AsUpdate(new
                {
                    todo.Name,
                    todo.Description,
                }).ToSql();

            await db.ExecuteAsync(query);
        }

        public async Task Delete(int id)
        {
            var query = new Query("Todo")
                .Where("Id", id)
                .AsDelete().ToSql();

            await db.ExecuteAsync(query);
        }
    }
}