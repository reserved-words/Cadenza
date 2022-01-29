using Microsoft.JSInterop;

namespace Cadenza.Source.Spotify;

public class SpotifyInterop : IErrorHandler
{
    private readonly IJSRuntime _js;

    public SpotifyInterop(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<bool> DeviceNotFound()
    {
        return await _js.InvokeAsync<bool>("spotifyDeviceNotFound");
    }

    public async Task<bool> SpotifyConnect()
    {
        return await _js.InvokeAsync<bool>("spotifyConnect");
    }

    public async Task UnexpectedError()
    {
        await _js.InvokeAsync<bool>("spotifyUnexpectedError");
    }
}
