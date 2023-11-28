namespace Cadenza.Web.Actions.Effects;

public class PlayHistoryEffects
{
    private const int MaxItems = 5;

    private readonly IArtworkFetcher _artworkFetcher;
    private readonly IPlayHistory _history;

    public PlayHistoryEffects(IPlayHistory history, IArtworkFetcher artworkFetcher)
    {
        _history = history;
        _artworkFetcher = artworkFetcher;
    }

    [EffectMethod]
    public async Task HandleFetchPlayHistoryAlbumsRequest(FetchPlayHistoryAlbumsRequest action, IDispatcher dispatcher)
    {
        var result = await _history.GetTopAlbums(action.Period, MaxItems);

        var list = result.ToList();

        var listWithImages = list.Select(t => t with
        {
            ImageUrl = _artworkFetcher.GetAlbumArtworkSrc(t.Id)
        })
        .ToList();

        dispatcher.Dispatch(new FetchPlayHistoryAlbumsResult(action.Period, listWithImages));
    }

    [EffectMethod]
    public async Task HandleFetchPlayHistoryArtistsRequest(FetchPlayHistoryArtistsRequest action, IDispatcher dispatcher)
    {
        var result = await _history.GetTopArtists(action.Period, MaxItems);
        dispatcher.Dispatch(new FetchPlayHistoryArtistsResult(action.Period, result.ToList()));
    }

    [EffectMethod]
    public async Task HandleFetchPlayHistoryTracksRequest(FetchPlayHistoryTracksRequest action, IDispatcher dispatcher)
    {
        var result = await _history.GetTopTracks(action.Period, MaxItems);
        dispatcher.Dispatch(new FetchPlayHistoryTracksResult(action.Period, result.ToList()));
    }
}
