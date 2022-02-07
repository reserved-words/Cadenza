namespace Cadenza.Spotify.API.Interfaces;

internal interface IApiHelper
{
    Task<ApiResponse<T>> Get<T>(string url) where T : class;
}