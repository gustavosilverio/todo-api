using System.Text.Json.Serialization;

namespace TodoApi.Model.Request.Todo
{
    public class CreateTodoRequest
    {
        public required int UserId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
