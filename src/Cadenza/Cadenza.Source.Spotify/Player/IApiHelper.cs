namespace Cadenza.Source.Spotify.Player;

public interface IApiHelper
{
    Task<ApiResponse> Put(string urlFormat, object data = null);
    Task<ApiResponse<T>> Get<T>(string url) where T : class;
}