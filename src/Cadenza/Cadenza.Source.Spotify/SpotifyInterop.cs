﻿using Microsoft.JSInterop;

namespace Cadenza.Source.Spotify;

public class SpotifyInterop : IErrorHandler, ISpotifyStartup
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

    public async Task<bool> ConnectPlayer(string accessToken)
    {
        return await _js.InvokeAsync<bool>("connectSpotifyPlayer", accessToken);
    }

    public async Task UnexpectedError()
    {
        await _js.InvokeAsync<bool>("spotifyUnexpectedError");
    }
}
