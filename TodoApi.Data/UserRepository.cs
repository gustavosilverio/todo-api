using Microsoft.Extensions.Configuration;
using PetaPoco.SqlKata;
using SqlKata;
using TodoApi.Models;
using TodoApi.Data.Interfaces;
using TodoApi.Model.Request.User;

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

        public async Task<User> GetByEmail(string email)
        {
            var query = new Query("User")
                .Where("Email", email).ToSql();

            return await db.FirstOrDefaultAsync<User>(query);
        }
    }
}
