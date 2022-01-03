using Microsoft.Extensions.Options;

namespace Cadenza.Source.Local;

public class LocalPlayer : IAudioPlayer
{
    private readonly IOptions<LocalApiSettings> _settings;
    private readonly IAudioPlayer _audioPlayer;

    public LocalPlayer(IAudioPlayer audioPlayer, IOptions<LocalApiSettings> settings)
    {
        _audioPlayer = audioPlayer;
        _settings = settings;
    }

    public async Task<int> Resume()
    {
        var millisecondsPlayed = await _audioPlayer.Resume();
        return millisecondsPlayed / 1000;
    }

    public async Task<int> Pause()
    {
        var millisecondsPlayed = await _audioPlayer.Pause();
        return millisecondsPlayed / 1000;
    }

    public async Task Play(string id)
    {
        var uri = string.Format(_settings.GetApiEndpoint(e => e.PlayTrackUrl), id);
        await _audioPlayer.Play(uri);
    }

    public async Task<int> Stop()
    {
        var millisecondsPlayed = await _audioPlayer.Stop();
        return millisecondsPlayed / 1000;
    }
}
