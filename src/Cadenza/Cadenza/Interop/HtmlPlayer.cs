using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Common.Model;

namespace Cadenza.Interop;

internal class HtmlPlayer : IAudioPlayer
{
    private readonly IJSRuntime _js;

    public HtmlPlayer(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<TrackProgress> Pause()
    {
        return await _js.InvokeAsync<TrackProgress>("pause");
    }

    public async Task Play(string uri)
    {
        await _js.InvokeAsync<TrackProgress>("play", uri);
    }

    public async Task<TrackProgress> Resume()
    {
        return await _js.InvokeAsync<TrackProgress>("play");
    }

    public async Task<TrackProgress> Stop()
    {
        return await _js.InvokeAsync<TrackProgress>("stop");
    }
}
