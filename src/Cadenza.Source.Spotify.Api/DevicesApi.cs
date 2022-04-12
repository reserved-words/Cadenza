using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Api.Internal;
using Cadenza.Source.Spotify.Api.Model.Player;

namespace Cadenza.Source.Spotify.Api;

internal class DevicesApi : IDevicesApi
{
    private const string AvailableDevicesUrl = "https://api.spotify.com/v1/me/player/devices";

    private readonly IApiHelper _api;

    public DevicesApi(IApiHelper api)
    {
        _api = api;
    }

    public async Task<SpotifyApiDevicesResponse> GetDevices()
    {
        var response = await _api.Get<SpotifyApiDevicesResponse>(AvailableDevicesUrl);
        return response.Data;
    }
}
