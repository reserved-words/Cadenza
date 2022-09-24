namespace Cadenza.Utilities.Exceptions;

public class HttpException : Exception
{
    public HttpException(HttpResponseMessage response)
        : base($"{response.StatusCode} ({response.RequestMessage.RequestUri})")
    {
    }

    public HttpExceptionType Type { get; }
}

public enum HttpExceptionType
{
    UnexpectedError
}