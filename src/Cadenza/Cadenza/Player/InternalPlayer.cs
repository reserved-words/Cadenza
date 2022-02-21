using Cadenza.Common;

namespace Cadenza.Source.Spotify.Player;

public class InternalPlayer : IAudioPlayer
{
    private readonly IPlayerApi _api;

    internal InternalPlayer(IPlayerApi api)
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

    public async Task<TrackProgress> Stop()
    {
        var progress = await GetProgress();
        await _api.Pause();
        return progress;
    }

    private async Task<TrackProgress> GetProgress()
    {
        var playState = await _api.GetPlayState();

        if (playState == null || playState.item == null)
            return new TrackProgress(-1, -1);

        return GetProgress(playState);
    }

    private static TrackProgress GetProgress(SpotifyApiPlayState playState)
    {
        return new TrackProgress(
            MillisecondsToSeconds(playState.progress_ms),
            MillisecondsToSeconds(playState.item.duration_ms));
    }

    private static int MillisecondsToSeconds(int? milliseconds)
    {
        return (milliseconds ?? 0) / 1000;
    }
}