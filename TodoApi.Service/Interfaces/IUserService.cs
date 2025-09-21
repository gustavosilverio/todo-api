using TodoApi.Models;
using TodoApi.Models.Request;

namespace TodoApi.Services.Interfaces
{
    public interface IUserService
    {
        Task Create(CreateUserRequest user);
        Task<User> GetById(int id);
    }
}
