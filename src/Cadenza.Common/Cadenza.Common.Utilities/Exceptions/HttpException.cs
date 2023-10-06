using System.Net;

namespace Cadenza.Common.Utilities.Exceptions;

public class HttpException : Exception
{
    public HttpException(Uri requestUri, HttpStatusCode statusCode, string responseContent)
        : base($"HTTP request exception - {statusCode}")
    {
        RequestUri = requestUri;
        StatusCode = statusCode;
        ResponseContent = responseContent;
    }

    public Uri RequestUri { get; }
    public HttpStatusCode StatusCode { get; }
    public string ResponseContent { get; }
}
