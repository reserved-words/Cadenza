namespace Cadenza.Source.Local;

public class LocalPlayer : IAudioPlayer
{
    private readonly ILocalApiConfig _apiConfig;
    private readonly IAudioPlayer _audioPlayer;

    public LocalPlayer(IAudioPlayer audioPlayer, ILocalApiConfig apiConfig)
    {
        _audioPlayer = audioPlayer;
        _apiConfig = apiConfig;
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
        var uri = string.Format(_apiConfig.TrackUriFormat, id);
        await _audioPlayer.Play(uri);
    }

    public async Task<int> Stop()
    {
        var millisecondsPlayed = await _audioPlayer.Stop();
        return millisecondsPlayed / 1000;
    }
}
