using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TodoApi.Models;
using TodoApi.Service.Interfaces;
using TodoApi.Util.Exceptions;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TodoApi.Service
{
    internal sealed class TokenService(IConfiguration config) : ITokenService
    {
        public string CreateToken(User user)
        {
            string secretKey = config["Jwt:SecretKey"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new (JwtRegisteredClaimNames.Email, user.Email),
                    new (JwtRegisteredClaimNames.Name, user.Name),
                ]),
                Expires = DateTime.UtcNow.AddMinutes(config.GetValue<int>("Jwt:AccessTokenExpirationInMinutes")),
                SigningCredentials = credentials,
                Issuer = config["Jwt:Issuer"],
                Audience = config["Jwt:Audience"],
            };
            
            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDescriptor);

            return token;
        }

        public (string refreshToken, DateTime refreshTokenExpiryTime) CreateRefreshToken()
        {
            var randomNumber = new Byte[32];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            var refreshToken = Convert.ToBase64String(randomNumber);
            var refreshTokenExpiryTime = DateTime.UtcNow.AddDays(int.Parse(config["Jwt:RefreshTokenExpirationInDays"]!));

            return (refreshToken, refreshTokenExpiryTime);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]!)),
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ResponseException("Invalid token");
            }
            
            return principal;
        }
    }
}
