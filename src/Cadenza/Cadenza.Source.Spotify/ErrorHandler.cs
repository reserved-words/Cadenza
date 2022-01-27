using Microsoft.JSInterop;

namespace Cadenza.Source.Spotify;

public class ErrorHandler : IErrorHandler
{
    private readonly IJSRuntime _js;

    public ErrorHandler(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<bool> DeviceNotFound()
    {
        return await _js.InvokeAsync<bool>("spotifyDeviceNotFound");
    }

    public async Task UnexpectedError()
    {
        await _js.InvokeAsync<bool>("spotifyUnexpectedError");
    }
}
