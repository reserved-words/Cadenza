namespace Cadenza.API.Wrapper;

public class HttpException : Exception
{
    // TODO: Less generic error message
    public HttpException(HttpExceptionType type = HttpExceptionType.UnexpectedError) 
        : base("Failed to connect to Cadenza API")
    {
        Type = type;
    }

    public HttpExceptionType Type { get; }
}

public enum HttpExceptionType
{
    UnexpectedError
}