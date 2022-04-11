namespace Cadenza.Source.Spotify.Interfaces;

internal interface IApiHelper
{
    Task<ApiResponse<T>> Get<T>(string url) where T : class;
    Task<ApiResponse<T>> Get<T>(string url, string accessToken) where T : class;
    Task<ApiResponse> Post(string url, object data = null);
    Task<ApiResponse> Put(string url, string accessToken, object data = null);
    Task<ApiResponse> Put(string url, object data = null);
}

public class ApiErrorDetails
{
    public int Status { get; set; }
    public string Message { get; set; }
}

public class ApiError
{
    public ApiErrorDetails Error { get; set; }
}

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
