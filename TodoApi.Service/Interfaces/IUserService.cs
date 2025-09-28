using TodoApi.Model.DTO;
using TodoApi.Model.Request.User;
using TodoApi.Models;

namespace TodoApi.Services.Interfaces
{
    public interface IUserService
    {
        Task Create(CreateUserRequest user);
        Task<List<SafeUserDTO>> GetAll();
        Task<User> GetById(int id);
    }
}
