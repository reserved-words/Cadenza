using Microsoft.JSInterop;

namespace Cadenza.Player;

public class HtmlPlayer : IAudioPlayer
{
    private readonly IJSRuntime _js;

    public HtmlPlayer(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<int> Pause()
    {
        return await _js.InvokeAsync<int>("pause");
    }

    public async Task Play(string uri)
    {
        await _js.InvokeVoidAsync("play", uri);
    }

    public async Task<int> Resume()
    {
        return await _js.InvokeAsync<int>("play");
    }

    public async Task<int> Stop()
    {
        return await _js.InvokeAsync<int>("stop");
    }
}