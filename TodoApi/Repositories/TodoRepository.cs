using PetaPoco.SqlKata;
using SqlKata;
using SqlKata.Compilers;
using TodoApi.Models;
using TodoApi.Models.Request;
using TodoApi.Repositories.Interfaces;

namespace TodoApi.Repositories
{
    public class TodoRepository(IConfiguration config) : BaseRepository(config), ITodoRepository
    {
        public async Task Salvar(TodoRequest todo)
        {
            await db.InsertAsync("Todo", todo);
        }

        public async Task<List<Todo>> GetAll()
        {
            var query = new Query("Todo");

            return await db.FetchAsync<Todo>(query.ToSql<SqlServerCompiler>());
        }
    }
}
