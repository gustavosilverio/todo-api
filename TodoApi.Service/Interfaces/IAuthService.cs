using TodoApi.Model.Request.Auth;

namespace TodoApi.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(LoginAuthRequest request);
    }
}
