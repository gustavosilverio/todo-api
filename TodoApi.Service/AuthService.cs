using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
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
            if (string.IsNullOrEmpty(request.Email))
                throw new ResponseException("Email not provided");

            if (string.IsNullOrEmpty(request.Password))
                throw new ResponseException("Password not provided");

            var user = await userRepository.GetByEmail(request.Email) ?? throw new ResponseException("User not found");

            if (!passwordHash.VerifyPassword(user.Password, request.Password))
                throw new ResponseException("Invalid password");

            var accessToken = tokenService.CreateToken(user);
            var (refreshToken, refreshTokenExpiryTime) = tokenService.CreateRefreshToken();
            
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = refreshTokenExpiryTime;
            user.UpdatedAt = DateTime.UtcNow;

            await userRepository.Update(user);

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
                RefreshTokenExpiryTime = refreshTokenExpiryTime,
            };
        }

        public async Task<LoginResponse> RefreshToken(RefreshTokenRequest request)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);

            var userId = int.Parse(principal.FindFirstValue(JwtRegisteredClaimNames.Sub)!);

            if (userId == 0)
                throw new ResponseException("Invalid token");

            var user = await userRepository.GetById(userId);

            if (user is null || user.RefreshToken != request.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new ResponseException("Invalid request");
            }

            var newAccessToken = tokenService.CreateToken(user);
            var (newRefreshToken, refreshTokenExpiryTime) = tokenService.CreateRefreshToken();
            
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = refreshTokenExpiryTime;
            user.UpdatedAt = DateTime.UtcNow;
            
            await userRepository.Update(user);

            return new LoginResponse()
            {
                UserCredentials = new()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                },
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
                RefreshTokenExpiryTime = refreshTokenExpiryTime,
            };
        }

        public async Task Revoke(int userId)
        {
            var _ = await userRepository.GetById(userId) ?? throw new ResponseException("User not found");
            
            await userRepository.RevokeUser(userId);
        }

        public void RevokeAll() => userRepository.RevokeAllUser();
    }
}
