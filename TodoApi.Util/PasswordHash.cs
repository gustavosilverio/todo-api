using Microsoft.AspNetCore.Identity;
using TodoApi.Models;
using TodoApi.Util.Interfaces;

namespace TodoApi.Util
{
    public class PasswordHash(IPasswordHasher<User> _passwordHasher) : IPasswordHash
    {
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
