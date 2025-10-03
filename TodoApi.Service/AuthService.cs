using System.IdentityModel.Tokens.Jwt;
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

            var accessToken = tokenService.CreateToken(user);
            var refreshToken = tokenService.GenerateRefreshToken();
            
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            
            userRepository.Update(user);

            return new()
            {
                UserCredentials = new()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                },
                Token = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task<LoginResponse> RefreshToken(RefreshTokenRequest request)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            var userId = Int32.Parse(principal.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault()?.Value);

            var user = await userRepository.GetById(userId);

            if (user is null || user.RefreshToken != request.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new ResponseException("Invalid cliente request");
            }

            var newAccessToken = tokenService.CreateToken(user);
            var newRefreshToken = tokenService.GenerateRefreshToken();
            
            user.RefreshToken = newRefreshToken;
            userRepository.Update(user);

            return new LoginResponse()
            {
                UserCredentials = new()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                },
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
