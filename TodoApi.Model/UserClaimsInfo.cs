using Microsoft.AspNetCore.Http;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TodoApi.Model
{
    public class UserClaimsInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public UserClaimsInfo(IHttpContextAccessor accessor)
        {
            var user = accessor.HttpContext?.User;

            if (user is not null || user?.Identity?.IsAuthenticated == true)
            {
                string? userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                Name = user.FindFirst(JwtRegisteredClaimNames.Name)?.Value ?? string.Empty;
                Email = user.FindFirst(JwtRegisteredClaimNames.Email)?.Value ?? string.Empty;
                
                if (int.TryParse(userId, out int id))
                {
                    Id = id;
                }
            }
        }
    }
}
