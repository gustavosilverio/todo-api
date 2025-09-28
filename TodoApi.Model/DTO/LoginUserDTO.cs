namespace TodoApi.Model.DTO
{
    public class LoginUserDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
