using Cadenza.Source.Spotify.Model;

namespace Cadenza.Source.Spotify.Interfaces;

internal interface IApiHelper
{
    Task<ApiResponse> Put(string url, string accessToken, object data = null);
    Task<ApiResponse<T>> Get<T>(string url, string accessToken) where T : class;
}