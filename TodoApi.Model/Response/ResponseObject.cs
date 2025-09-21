using Newtonsoft.Json;
using System.Net;

namespace TodoApi.Models.Response
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ResponseObject<T>(HttpStatusCode code, T? body = default, string? message = null)
    {
        public bool Success { get; set; } = (int)code < 400;
        public T? Data { get; set; } = body;
        public string? Message { get; set; } = message;
    }
}
