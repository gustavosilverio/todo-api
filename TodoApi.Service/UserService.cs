using TodoApi.Models;
using TodoApi.Models.Request;
using TodoApi.Services.Interfaces;
using TodoApi.Data.Interfaces;

namespace TodoApi.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task Create(CreateUserRequest user) => await userRepository.Create(user);
        public async Task<User> GetById(int id) => await userRepository.GetById(id);
    }
}
