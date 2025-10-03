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

        public void Update(User user)
        {
            db.Update("User", "Id", user);
        }
        
        public async Task Delete(int id)
        {
            var query = new Query("User")
                .Where("Id", id)
                .AsDelete().ToSql();
            await db.ExecuteAsync(query);
        }
        
        public async Task<List<User>> GetAll()
        {
            var query = new Query("User").ToSql();

            return await db.FetchAsync<User>(query);
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
