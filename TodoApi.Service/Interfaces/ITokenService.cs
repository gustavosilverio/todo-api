using System.Security.Claims;
using TodoApi.Models;

namespace TodoApi.Service.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
        (string refreshToken, DateTime refreshTokenExpiryTime) CreateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
