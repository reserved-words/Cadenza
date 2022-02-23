using Cadenza.Source.Spotify.Interfaces;
using Cadenza.Source.Spotify.Model;

namespace Cadenza.Source.Spotify.Api;

internal class DevicesApi : IDevicesApi
{
    private const string AvailableDevicesUrl = "https://api.spotify.com/v1/me/player/devices";

    private readonly IApiHelper _api;

    public DevicesApi(IApiHelper api)
    {
        _api = api;
    }

    public async Task<SpotifyApiDevicesResponse> GetDevices(string accessToken)
    {
        var response = await _api.Get<SpotifyApiDevicesResponse>(AvailableDevicesUrl, accessToken);
        return response.Data;
    }

}