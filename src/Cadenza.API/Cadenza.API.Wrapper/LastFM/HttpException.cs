namespace Cadenza.API.Wrapper.LastFM;

public class HttpException : Exception
{
    // TODO: Less generic error message
    public HttpException() : base("Failed to connect to Cadenza API")
    {
    }
}