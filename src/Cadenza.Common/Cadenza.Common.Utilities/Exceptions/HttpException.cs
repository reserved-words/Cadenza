namespace Cadenza.Common.Utilities.Exceptions;

public class HttpException : Exception
{
    public HttpException(HttpResponseMessage response)
        : base($"{response.StatusCode} ({response.RequestMessage.RequestUri})")
    {
    }

    public HttpException(string error)
        : base(error)
    {
    }
}
