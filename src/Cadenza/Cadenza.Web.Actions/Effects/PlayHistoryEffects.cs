namespace Cadenza.Web.Actions.Effects;

public class PlayHistoryEffects
{
    private const int MaxItems = 12;

    private readonly IArtworkApi _artworkApi;
    private readonly IHistoryApi _historyApi;

    public PlayHistoryEffects(IHistoryApi historyApi, IArtworkApi artworkApi)
    {
        _historyApi = historyApi;
        _artworkApi = artworkApi;
    }

    [EffectMethod]
    public async Task HandleFetchPlayHistoryAlbumsRequest(FetchPlayHistoryAlbumsRequest action, IDispatcher dispatcher)
    {
        var result = await _historyApi.GetTopAlbums(action.Period, MaxItems);

        var list = result.ToList();

        var listWithImages = list.Select(t => t with
        {
            ImageUrl = _artworkApi.GetAlbumArtworkUrl(t.Id)
        })
        .ToList();

        dispatcher.Dispatch(new FetchPlayHistoryAlbumsResult(action.Period, listWithImages));
    }

    [EffectMethod]
    public async Task HandleFetchPlayHistoryArtistsRequest(FetchPlayHistoryArtistsRequest action, IDispatcher dispatcher)
    {
        var result = await _historyApi.GetTopArtists(action.Period, MaxItems);
        dispatcher.Dispatch(new FetchPlayHistoryArtistsResult(action.Period, result.ToList()));
    }

    [EffectMethod]
    public async Task HandleFetchPlayHistoryTracksRequest(FetchPlayHistoryTracksRequest action, IDispatcher dispatcher)
    {
        var result = await _historyApi.GetTopTracks(action.Period, MaxItems);
        dispatcher.Dispatch(new FetchPlayHistoryTracksResult(action.Period, result.ToList()));
    }
}
