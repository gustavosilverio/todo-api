using System.Security.Claims;
using TodoApi.Models;

namespace TodoApi.Service.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
