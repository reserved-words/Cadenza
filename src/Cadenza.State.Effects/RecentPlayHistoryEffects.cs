namespace Cadenza.State.Effects;

public class RecentPlayHistoryEffects
{
    private const int MaxItems = 15;
    private const int MinMinutesPlayed = 4;
    private const int MinPercentagePlayed = 50;

    private readonly IHistory _history;
    private readonly IPlayTracker _tracker;

    public RecentPlayHistoryEffects(IHistory history, IPlayTracker tracker)
    {
        _history = history;
        _tracker = tracker;
    }

    [EffectMethod]
    public async Task HandleUpdateRecentPlayHistoryRequest(UpdateRecentPlayHistoryRequest action, IDispatcher dispatcher)
    {
        var progress = new TrackProgress(action.SecondsPlayed, action.Track.Duration);
        
        if (action.Status == PlayStatus.Playing)
        {
            await UpdateNowPlaying(action.Track, progress.SecondsRemaining);
        }
        else if (action.Status == PlayStatus.Paused)
        {
            await UpdateNowPlaying(action.Track, 1);
        }
        else if (action.Status == PlayStatus.Stopped && action.Track != null)
        {
            await RecordPlay(action.Track, progress);
        }

        dispatcher.Dispatch(new UpdateRecentPlayHistoryResult());
    }

    [EffectMethod(typeof(UpdateRecentPlayHistoryResult))]
    public Task HandleUpdateRecentPlayHistoryResult(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new FetchRecentPlayHistoryRequest());
        return Task.CompletedTask;
    }

    [EffectMethod(typeof(FetchRecentPlayHistoryRequest))]
    public async Task HandleFetchRecentPlayHistoryRequest(IDispatcher dispatcher)
    {
        var result = await _history.GetRecentTracks(MaxItems, 1); 
        dispatcher.Dispatch(new FetchRecentPlayHistoryResult(result.ToList()));
    }

    private async Task RecordPlay(TrackFull track, TrackProgress progress)
    {
        if (track == null)
            return;

        if (!PlayedEnough(progress))
            return;

        await _tracker.RecordPlay(track, DateTime.Now);
    }

    private async Task UpdateNowPlaying(TrackFull track, int secondsRemaining)
    {
        if (track == null)
            return;

        await _tracker.UpdateNowPlaying(track, secondsRemaining);
    }

    private static bool PlayedEnough(TrackProgress progress)
    {
        return progress.SecondsPlayed >= MinMinutesPlayed * 60
            || progress.PercentagePlayed >= MinPercentagePlayed;
    }
}
