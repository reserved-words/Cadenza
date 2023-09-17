using Cadenza.State.Actions;
using Cadenza.Web.Common.Interfaces;
using Fluxor;

namespace Cadenza.State.Effects;

public class RecentPlayHistoryEffects
{
    private readonly IHistory _history;

    public RecentPlayHistoryEffects(IHistory history)
    {
        _history = history;
    }

    [EffectMethod(typeof(UpdateRecentPlayHistoryRequest))]
    public async Task HandleUpdateRecentPlayHistoryRequest(IDispatcher dispatcher)
    {
        // Do scrobbling / update now playing here
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
        var result = await _history.GetRecentTracks(20, 1); 
        dispatcher.Dispatch(new FetchRecentPlayHistoryResult(result.ToList()));
    }
}
