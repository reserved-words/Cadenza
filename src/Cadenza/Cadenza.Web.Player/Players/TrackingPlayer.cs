using Cadenza.State.Store;
using Fluxor;

namespace Cadenza.Web.Player.Players;

internal class TrackingPlayer : IUtilityPlayer
{
    private readonly IPlayTracker _tracker;
    private readonly IState<CurrentTrackState> _currentTrackState;

    public TrackingPlayer(IPlayTracker tracker, IState<CurrentTrackState> currentTrackState)
    {
        _tracker = tracker;
        _currentTrackState = currentTrackState;
    }

    public async Task OnPlay(TrackProgress progress)
    {
        await UpdateNowPlaying(progress);
    }

    public async Task OnPause(TrackProgress progress)
    {
        await UpdateNowPlaying(null);
    }

    public async Task OnResume(TrackProgress progress)
    {
        await UpdateNowPlaying(progress);
    }

    public async Task OnStop(TrackProgress progress)
    {
        await RecordPlay(progress);
    }

    private async Task RecordPlay(TrackProgress progress)
    {
        var currentTrack = _currentTrackState.Value.FullTrack;

        if (currentTrack == null)
            return;

        if (!PlayedEnough(progress))
            return;

        await _tracker.RecordPlay(currentTrack, DateTime.Now);
    }

    private async Task UpdateNowPlaying(TrackProgress progress)
    {
        var currentTrack = _currentTrackState.Value.FullTrack;

        if (currentTrack == null)
            return;

        await _tracker.UpdateNowPlaying(currentTrack, progress?.SecondsRemaining ?? 1);
    }

    private static bool PlayedEnough(TrackProgress progress)
    {
        return progress.SecondsPlayed >= 4 * 60
            || progress.PercentagePlayed >= 50;
    }
}