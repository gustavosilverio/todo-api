namespace TodoApi.Models.Request
{
    public class TodoRequest
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
