using TodoApi.Models;

namespace TodoApi.Service.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
