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

    [EffectMethod(typeof(FetchRecentPlayHistoryAction))]
    public async Task HandleFetchRecentPlayHistoryAction(IDispatcher dispatcher)
    {
        var result = await _history.GetRecentTracks(20, 1); 
        dispatcher.Dispatch(new FetchRecentPlayHistoryResultAction(result.ToList()));
    }
}
