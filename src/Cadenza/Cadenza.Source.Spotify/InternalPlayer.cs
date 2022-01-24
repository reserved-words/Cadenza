namespace Cadenza.Source.Spotify;

public class InternalPlayer : IAudioPlayer
{
    private readonly ISpotifyPlayerApi _api;

    internal InternalPlayer(ISpotifyPlayerApi api)
    {
        _api = api;
    }

    public async Task<TrackProgress> Resume()
    {
        await _api.Play();
        return await GetProgress();
    }

    public async Task<TrackProgress> Pause()
    {
        await _api.Pause();
        return await GetProgress();
    }

    public async Task Play(string trackId)
    {
        await _api.Play(trackId);
    }

    public async Task Stop()
    {
        await _api.Pause();
    }

    private async Task<TrackProgress> GetProgress()
    {
        var playState = await _api.GetPlayState();
        return new TrackProgress(
            MillisecondsToSeconds(playState?.progress_ms ?? 0), 
            MillisecondsToSeconds(playState?.item?.duration_ms ?? 0));
    }

    private static int MillisecondsToSeconds(int? milliseconds)
    {
        return (milliseconds ?? 0) / 1000;
    }
}