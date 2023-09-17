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

    [EffectMethod(typeof(FetchRecentPlayHistoryRequest))]
    public async Task HandleFetchRecentPlayHistoryAction(IDispatcher dispatcher)
    {
        await Task.Delay(2000);
        var result = await _history.GetRecentTracks(20, 1); 
        dispatcher.Dispatch(new FetchRecentPlayHistoryAction(result.ToList()));
    }
}
