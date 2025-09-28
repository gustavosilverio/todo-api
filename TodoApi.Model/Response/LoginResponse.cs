using TodoApi.Model.DTO;

namespace TodoApi.Model.Response
{
    public class LoginResponse
    {
        public required LoginUserDTO UserCredentials { get; set; }
        public required string Token { get; set; }
    }
}
