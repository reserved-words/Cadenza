namespace Cadenza.API.Wrapper.LastFM;

public class HttpException : Exception
{
    // TODO: Less generic error message
    public HttpException() : base("Could not connect to API")
    {
    }
}