using Microsoft.IdentityModel.Tokens;
using TodoApi.Data.Interfaces;
using TodoApi.Model.Request.Auth;
using TodoApi.Model.Response;
using TodoApi.Service.Interfaces;
using TodoApi.Util.Exceptions;
using TodoApi.Util.Interfaces;

namespace TodoApi.Service
{
    public class AuthService(IUserRepository userRepository, ITokenService tokenService, IPasswordHash passwordHash) : IAuthService
    {
        public async Task<LoginResponse> Login(LoginAuthRequest request)
        {
            if (request.Email.IsNullOrEmpty())
                throw new ResponseException("Email not provided");

            if (request.Password.IsNullOrEmpty())
                throw new ResponseException("Password not provided");

            var user = await userRepository.GetByEmail(request.Email) ?? throw new ResponseException("User not found");

            if (!passwordHash.VerifyPassword(user.Password, request.Password))
                throw new ResponseException("Invalid password");

            var token = tokenService.CreateToken(user);

            return new()
            {
                UserCredentials = new()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                },
                Token = token,
            };
        }
    }
}
