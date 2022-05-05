using Cadenza.Core.Model;
using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Interfaces;

namespace Cadenza.Source.Spotify.Api;

internal class ProgressService : IProgressService
{

    private readonly IPlayerApi _api;

    public ProgressService(IPlayerApi api)
    {
        _api = api;
    }

    public async Task<TrackProgress> GetProgress()
    {
        var playState = await _api.GetPlayState();

        if (playState == null || playState.item == null)
            return new TrackProgress(-1, -1);

        return new TrackProgress(
            MillisecondsToSeconds(playState.progress_ms),
            MillisecondsToSeconds(playState.item.duration_ms));
    }

    private static int MillisecondsToSeconds(int? milliseconds)
    {
        return (milliseconds ?? 0) / 1000;
    }
}
