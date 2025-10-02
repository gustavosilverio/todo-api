using TodoApi.Model.DTO;
using TodoApi.Model.Request.User;
using TodoApi.Models;

namespace TodoApi.Service.Interfaces
{
    public interface IUserService
    {
        Task Create(CreateUserRequest user);
        Task Delete(int id);
        Task<List<SafeUserDTO>> GetAll();
        Task<User> GetById(int id);
    }
}
