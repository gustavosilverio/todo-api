namespace TodoApi.Model.Request.Auth
{
    public class LoginAuthRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}