namespace Cadenza.Web.Actions.Effects;

public class RecentHistoryEffects
{
    private const int MaxItems = 12;

    private readonly IArtworkApi _artworkApi;
    private readonly IHistoryApi _historyApi;

    public RecentHistoryEffects(IHistoryApi historyApi, IArtworkApi artworkApi)
    {
        _historyApi = historyApi;
        _artworkApi = artworkApi;
    }

    [EffectMethod]
    public async Task HandleFetchPlaylistHistoryAlbumsRequest(FetchPlaylistHistoryAlbumsRequest action, IDispatcher dispatcher)
    {
        var result = await _historyApi.GetRecentlyPlayedAlbums(MaxItems);
        
        var resultWithImages = result.Select(album => album with
        {
            ImageUrl = _artworkApi.GetAlbumArtworkUrl(album.Id)
        })
        .ToList();

        dispatcher.Dispatch(new FetchPlaylistHistoryAlbumsResult(resultWithImages));
    }

    [EffectMethod]
    public async Task HandleRecentlyAddedAlbumsRequest(FetchRecentlyAddedAlbumsRequest action, IDispatcher dispatcher)
    {
        var result = await _historyApi.GetRecentlyAddedAlbums(MaxItems);

        var resultWithImages = result.Select(album => album with
        {
            ImageUrl = _artworkApi.GetAlbumArtworkUrl(album.Id)
        })
        .ToList();

        dispatcher.Dispatch(new FetchRecentlyAddedAlbumsResult(resultWithImages));
    }
}
