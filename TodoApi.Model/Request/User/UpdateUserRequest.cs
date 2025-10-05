using System.Text.Json.Serialization;

namespace TodoApi.Model.Request.User
{
    public class UpdateUserRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
