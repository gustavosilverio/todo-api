using TodoApi.Model.Request.User;
using TodoApi.Models;

namespace TodoApi.Data.Interfaces
{
    public interface IUserRepository
    {
        Task Create(CreateUserRequest user);
        Task Delete(int id);
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByEmail(string email);
    }
}
