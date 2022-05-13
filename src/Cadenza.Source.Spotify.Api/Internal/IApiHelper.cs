using Cadenza.Source.Spotify.Api.Responses;

namespace Cadenza.Source.Spotify.Api.Internal;

internal interface IApiHelper
{
    Task<ApiResponse<T>> Get<T>(string url) where T : class;
    Task<ApiResponse> Put(string url, object data = null);
    Task<ApiResponse> Delete(string url, object data = null);
}
