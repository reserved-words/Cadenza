namespace Cadenza.Source.Spotify;

public interface ISpotifyApi
{
    Task<ApiResponse> Put(string urlFormat, object data = null);
    Task<ApiResponse<T>> Get<T>(string url) where T : class;
}