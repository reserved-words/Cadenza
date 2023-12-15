namespace Cadenza.Web.Actions.Effects;

public class RecentPlayHistoryEffects
{
    private const int MinMinutesPlayed = 4;
    private const int MinPercentagePlayed = 50;

    private readonly IHistoryApi _historyApi;

    public RecentPlayHistoryEffects(IHistoryApi historyApi)
    {
        _historyApi = historyApi;
    }

    [EffectMethod]
    public async Task HandleUpdateRecentPlayHistoryRequest(UpdateRecentPlayHistoryRequest action, IDispatcher dispatcher)
    {
        if (action.Track == null)
            return;

        var progress = new TrackProgress(action.SecondsPlayed, action.Track.Duration);

        if (action.Status == PlayStatus.Playing)
        {
            await UpdateNowPlaying(action.Track, progress.SecondsRemaining);
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
        dispatcher.Dispatch(new FetchRecentlyPlayedTracksRequest());
        return Task.CompletedTask;
    }

    private async Task RecordPlay(TrackFullVM track, TrackProgress progress)
    {
        if (track == null)
            return;

        if (!PlayedEnough(progress))
            return;

        await _historyApi.RecordPlay(track, DateTime.Now);
    }

    private async Task UpdateNowPlaying(TrackFullVM track, int secondsRemaining)
    {
        if (track == null)
            return;

        await _historyApi.UpdateNowPlaying(track, secondsRemaining);
    }

    private static bool PlayedEnough(TrackProgress progress)
    {
        return progress.SecondsPlayed >= MinMinutesPlayed * 60
            || progress.PercentagePlayed >= MinPercentagePlayed;
    }
}
