using Cadenza.Core;
using Cadenza.Core.Interfaces;
using Cadenza.Core.Model;
using Microsoft.Extensions.Options;

namespace Cadenza.Source.Local;

internal class LocalPlayer : ISourcePlayer
{
    private readonly LocalApiSettings _settings;
    private readonly IAudioPlayer _audioPlayer;
    private readonly IUrl _url;

    public LocalPlayer(IAudioPlayer audioPlayer, IOptions<LocalApiSettings> settings, IUrl url)
    {
        _audioPlayer = audioPlayer;
        _settings = settings.Value;
        _url = url;
    }

    public LibrarySource Source => LibrarySource.Local;

    public async Task<TrackProgress> Resume()
    {
        return await _audioPlayer.Resume();
    }

    public async Task<TrackProgress> Pause()
    {
        return await _audioPlayer.Pause();
    }

    public async Task Play(string id)
    {
        var uri = string.Format(_url.Build(_settings.BaseUrl, _settings.Endpoints.PlayTrackUrl), id);
        await _audioPlayer.Play(uri);
    }

    public async Task<TrackProgress> Stop()
    {
        return await _audioPlayer.Stop();
    }
}
