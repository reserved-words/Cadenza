using Cadenza.State.Actions;
using Cadenza.Web.Common.Interfaces;
using Fluxor;

namespace Cadenza.State.Effects;

public class PlayHistoryEffects
{
    private const int MaxItems = 5;

    private readonly IHistory _history;

    public PlayHistoryEffects(IHistory history)
    {
        _history = history;
    }

    [EffectMethod]
    public async Task HandleFetchPlayHistoryAlbumsRequest(FetchPlayHistoryAlbumsRequest action, IDispatcher dispatcher)
    {
        var result = await _history.GetPlayedAlbums(action.Period, MaxItems, 1); 
        dispatcher.Dispatch(new FetchPlayHistoryAlbumsResult(action.Period, result.ToList()));
    }

    [EffectMethod]
    public async Task HandleFetchPlayHistoryArtistsRequest(FetchPlayHistoryArtistsRequest action, IDispatcher dispatcher)
    {
        var result = await _history.GetPlayedArtists(action.Period, MaxItems, 1);
        dispatcher.Dispatch(new FetchPlayHistoryArtistsResult(action.Period, result.ToList()));
    }

    [EffectMethod]
    public async Task HandleFetchPlayHistoryTracksRequest(FetchPlayHistoryTracksRequest action, IDispatcher dispatcher)
    {
        var result = await _history.GetPlayedTracks(action.Period, MaxItems, 1);
        dispatcher.Dispatch(new FetchPlayHistoryTracksResult(action.Period, result.ToList()));
    }
}
