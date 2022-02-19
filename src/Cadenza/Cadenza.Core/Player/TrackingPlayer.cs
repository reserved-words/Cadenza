namespace Cadenza.Core;

public class TrackingPlayer : IUtilityPlayer
{
    private readonly IPlayTracker _tracker;
    private readonly IStoreGetter _store;

    public TrackingPlayer(IPlayTracker tracker, IStoreGetter store)
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

        if (currentTrack == null || !Tracking(currentTrack))
            return;

        if (!PlayedEnough(progress))
            return;

        await _tracker.RecordPlay(currentTrack, DateTime.Now);
    }

    private async Task UpdateNowPlaying(TrackProgress progress)
    {
        var currentTrack = await CurrentTrack();

        if (currentTrack == null || !Tracking(currentTrack))
            return;

        await _tracker.UpdateNowPlaying(currentTrack, progress?.SecondsRemaining ?? 1);
    }

    private static bool PlayedEnough(TrackProgress progress)
    {
        return progress.SecondsPlayed >= 4 * 60 
            || progress.PercentagePlayed >= 50;
    }

    private static bool Tracking(TrackFull track)
    {
        // Need a better way to do this - source profile
        return track.Track.Source != LibrarySource.Spotify;
    }

    private async Task<TrackFull> CurrentTrack()
    {
        var storedValue = await _store.GetValue<TrackFull>(StoreKey.CurrentTrack);
        return storedValue?.Value;
    }
}