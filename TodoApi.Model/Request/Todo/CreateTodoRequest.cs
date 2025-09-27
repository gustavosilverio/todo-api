namespace TodoApi.Model.Request.Todo
{
    public class CreateTodoRequest
    {
        public required int UserId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
