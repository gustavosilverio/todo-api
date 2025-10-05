using TodoApi.Model.Request.Auth;
using TodoApi.Model.Response;

namespace TodoApi.Service.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginAuthRequest request);
        Task<LoginResponse> RefreshToken(RefreshTokenRequest request);
        Task Revoke(int userId);
        void RevokeAll();
    }
}
