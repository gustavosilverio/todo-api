namespace TodoApi.Models
{
    public class Todo
    {
        public required int Id { get; set; }
        public required int UserId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool IsDone { get; set; }
    }
}