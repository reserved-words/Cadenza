using Cadenza.Web.Common.Interfaces.Store;
using Cadenza.Web.Common.Model;
using Cadenza.Web.Player.Interfaces;

namespace Cadenza.Web.Player.Players;

internal class TrackingPlayer : IUtilityPlayer
{
    private readonly IPlayTracker _tracker;
    private readonly ICurrentTrackStore _store;

    public TrackingPlayer(IPlayTracker tracker, ICurrentTrackStore store)
    {
        _tracker = tracker;
        _store = store;
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
        var currentTrack = await CurrentTrack();

        if (currentTrack == null)
            return;

        if (!PlayedEnough(progress))
            return;

        await _tracker.RecordPlay(currentTrack, DateTime.Now);
    }

    private async Task UpdateNowPlaying(TrackProgress progress)
    {
        var currentTrack = await CurrentTrack();

        if (currentTrack == null)
            return;

        await _tracker.UpdateNowPlaying(currentTrack, progress?.SecondsRemaining ?? 1);
    }

    private static bool PlayedEnough(TrackProgress progress)
    {
        return progress.SecondsPlayed >= 4 * 60
            || progress.PercentagePlayed >= 50;
    }

    private async Task<TrackFull> CurrentTrack()
    {
        return await _store.GetCurrentTrack();
    }
}