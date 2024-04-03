namespace Cadenza.Services;

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

    public async Task Play(string uri, string track, string artist, string playlist, string artworkUrl)
    {
        await _js.InvokeAsync<TrackProgress>("play", uri, track, artist, playlist, artworkUrl);
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
