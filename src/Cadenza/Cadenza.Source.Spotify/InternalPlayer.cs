namespace Cadenza.Source.Spotify;

public class InternalPlayer : IAudioPlayer
{
    private readonly ISpotifyPlayerApi _api;

    internal InternalPlayer(ISpotifyPlayerApi api)
    {
        _api = api;
    }

    public async Task<int> Resume()
    {
        await _api.Play();
        return await GetSecondsPlayed();
    }

    public async Task<int> Pause()
    {
        await _api.Pause();
        return await GetSecondsPlayed();
    }

    public async Task Play(string trackId)
    {
        await _api.Play(trackId);
    }

    public async Task<int> Stop()
    {
        await _api.Pause();
        return await GetSecondsPlayed();
    }

    private async Task<int> GetSecondsPlayed()
    {
        var playState = await _api.GetPlayState();
        var millisecondsPlayed = playState.progress_ms;
        return (millisecondsPlayed ?? 0) / 1000;
    }
}