using PetaPoco;

namespace TodoApi.Repositories
{
    public class BaseRepository
    {
        protected IDatabase db;

        protected BaseRepository(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("dbTodo")
                ?? throw new("A connection string não foi encontrada.");

            db = new Database(connectionString, "Microsoft.Data.SqlClient");
        }
    }
}
