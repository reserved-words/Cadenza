namespace Cadenza.Source.Spotify.Api.Responses;

public class ApiResponse
{
    public ApiResponse(ApiErrorDetails error = null)
    {
        Error = error;
    }

    public bool Success => Error == null;
    public ApiErrorDetails Error { get; }
}

public class ApiResponse<T>
{
    public ApiResponse(T data = default)
    {
        Data = data;
    }

    public ApiResponse(ApiError error)
    {
        Error = error;
    }

    public bool Success => Error == null;
    public T Data { get; }
    public ApiError Error { get; }
}
