using TodoApi.Models;
using TodoApi.Models.Request;

namespace TodoApi.Data.Interfaces
{
    public interface IUserRepository
    {
        Task Create(CreateUserRequest user);
        Task<User> GetById(int id);
    }
}
