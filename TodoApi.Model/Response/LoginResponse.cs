using TodoApi.Model.DTO;

namespace TodoApi.Model.Response
{
    public class LoginResponse
    {
        public required SafeUserDTO UserCredentials { get; set; }
        public required string Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}