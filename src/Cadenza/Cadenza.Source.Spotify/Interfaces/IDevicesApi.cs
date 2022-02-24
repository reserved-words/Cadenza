using Cadenza.Source.Spotify.Model;

namespace Cadenza.Source.Spotify.Interfaces;

internal interface IDevicesApi
{
    Task<SpotifyApiDevicesResponse> GetDevices(string accessToken);
}
