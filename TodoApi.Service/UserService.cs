using TodoApi.Models;
using TodoApi.Services.Interfaces;
using TodoApi.Data.Interfaces;
using TodoApi.Model.Request.User;

namespace TodoApi.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task Create(CreateUserRequest user) => await userRepository.Create(user);
        public async Task<User> GetById(int id) => await userRepository.GetById(id);
    }
}
