namespace TodoApi.Models
{
    public class Todo
    {
        public required int Id { get; set; }
        public required int UserId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required bool IsDone { get; set; }
        public required DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}