using Cadenza.Source.Spotify.Api.Model.Player;

namespace Cadenza.Source.Spotify.Api.Interfaces;

public interface IDevicesApi
{
    Task<SpotifyApiDevicesResponse> GetDevices();
}
