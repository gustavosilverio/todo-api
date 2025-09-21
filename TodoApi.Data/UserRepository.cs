using Microsoft.Extensions.Configuration;
using PetaPoco.SqlKata;
using SqlKata;
using TodoApi.Models;
using TodoApi.Models.Request;
using TodoApi.Data.Interfaces;

namespace TodoApi.Data
{
    public class UserRepository(IConfiguration config) : BaseRepository(config), IUserRepository
    {
        public async Task Create(CreateUserRequest user)
        {
            await db.InsertAsync("User", user);
        }

        public async Task<User> GetById(int id)
        {
            var query = new Query("User")
                .Where("Id", id).ToSql();

            return await db.FirstOrDefaultAsync<User>(query);
        }
    }
}
