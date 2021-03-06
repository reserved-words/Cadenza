using Cadenza.Source.Spotify.Interfaces;
using Microsoft.JSInterop;

namespace Cadenza.Source.Spotify.Services;

internal class SpotifyInterop : ISpotifyInterop
{
    private readonly IJSRuntime _js;

    public SpotifyInterop(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<bool> ConnectPlayer(string accessToken)
    {
        return await _js.InvokeAsync<bool>("connectSpotifyPlayer", accessToken);
    }
}
