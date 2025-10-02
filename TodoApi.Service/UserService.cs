using TodoApi.Data.Interfaces;
using TodoApi.Model.DTO;
using TodoApi.Model.Request.User;
using TodoApi.Models;
using TodoApi.Service.Interfaces;
using TodoApi.Util.Exceptions;
using TodoApi.Util.Interfaces;

namespace TodoApi.Service
{
    public class UserService(IUserRepository userRepository, IPasswordHash passwordHash) : IUserService
    {
        public async Task Create(CreateUserRequest user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
                throw new ResponseException("Name not provided");

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ResponseException("Email not provided");

            if (string.IsNullOrWhiteSpace(user.Password))
                throw new ResponseException("Password not provided");

            user.Password = passwordHash.HashPassword(user.Password);

            await userRepository.Create(user);
        }

        public async Task Delete(int id)
        {
          var user = await GetById(id);
          
          if (user is null)
              throw new ResponseException("User not found");
          
          await userRepository.Delete(id);
        }
        
        public async Task<List<SafeUserDTO>> GetAll()
        {
            var users = await userRepository.GetAll();

            List<SafeUserDTO> safeUsers = new();

            foreach(var user in users)
            {
                SafeUserDTO safeUser = new()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                };

                safeUsers.Add(safeUser);
            }

            return safeUsers;
        }
        public async Task<User> GetById(int id) => await userRepository.GetById(id);
    }
}
