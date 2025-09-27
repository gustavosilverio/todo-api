using TodoApi.Model.Request.User;
using TodoApi.Models;

namespace TodoApi.Data.Interfaces
{
    public interface IUserRepository
    {
        Task Create(CreateUserRequest user);
        Task<User> GetById(int id);
        Task<User> GetByEmail(string email);
    }
}
