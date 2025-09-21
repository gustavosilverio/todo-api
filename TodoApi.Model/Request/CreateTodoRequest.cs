namespace TodoApi.Models.Request
{
    public class CreateTodoRequest
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
