using System.Text.Json.Serialization;

namespace TodoApi.Model.Request.Todo
{
    public class UpdateTodoRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
