namespace TodoApi.Util.Exceptions
{
    public class ResponseException(string error) : Exception
    {
        public string Error { get; set; } = error;
    }
}
