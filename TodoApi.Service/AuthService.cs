using Microsoft.IdentityModel.Tokens;
using TodoApi.Data.Interfaces;
using TodoApi.Model.Request.Auth;
using TodoApi.Service.Interfaces;
using TodoApi.Util.Exceptions;

namespace TodoApi.Service
{
    public class AuthService(IUserRepository userRepository, ITokenService tokenService) : IAuthService
    {
        public async Task<string> Login(LoginAuthRequest request)
        {
            var errors = new List<string>();

            if (request.Email.IsNullOrEmpty())
                throw new ResponseException("Email not provided");

            if (request.Password.IsNullOrEmpty())
                throw new ResponseException("Password not provided");

            var user = await userRepository.GetByEmail(request.Email) ?? throw new ResponseException("User not found");

            if (user.Password != request.Password)
                throw new ResponseException("Invalid password");

            return tokenService.CreateToken(user);
        }
    }
}
