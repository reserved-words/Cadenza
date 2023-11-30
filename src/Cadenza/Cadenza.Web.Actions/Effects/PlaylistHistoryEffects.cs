namespace Cadenza.Web.Actions.Effects;

public class PlaylistHistoryEffects
{
    private const int MaxItems = 12;

    private readonly IArtworkFetcher _artworkFetcher;
    private readonly IHistoryRepository _history;

    public PlaylistHistoryEffects(IHistoryRepository history, IArtworkFetcher artworkFetcher)
    {
        _history = history;
        _artworkFetcher = artworkFetcher;
    }

    [EffectMethod]
    public Task HandleFetchPlaylistHistoryRequest(FetchPlaylistHistoryRequest action, IDispatcher dispatcher)
    {
        if (action.Playlist.Type == PlaylistType.Album)
        {
            dispatcher.Dispatch(new FetchPlaylistHistoryAlbumsRequest());
        }
        else if (action.Playlist.Type == PlaylistType.Tag)
        {
            dispatcher.Dispatch(new FetchPlaylistHistoryTagsRequest());
        }

        return Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleFetchPlaylistHistoryAlbumsRequest(FetchPlaylistHistoryAlbumsRequest action, IDispatcher dispatcher)
    {
        var result = await _history.GetRecentAlbums(MaxItems);
        
        var resultWithImages = result.Select(album => album with
        {
            ImageUrl = _artworkFetcher.GetAlbumArtworkSrc(album.Id)
        })
        .ToList();

        dispatcher.Dispatch(new FetchPlaylistHistoryAlbumsResult(resultWithImages));
    }

    [EffectMethod]
    public async Task HandleFetchPlaylistHistoryTagasRequest(FetchPlaylistHistoryTagsRequest action, IDispatcher dispatcher)
    {
        var result = await _history.GetRecentTags(MaxItems);
        dispatcher.Dispatch(new FetchPlaylistHistoryTagsResult(result));
    }
}
