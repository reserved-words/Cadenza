namespace Cadenza.Web.Actions.Effects;

public class PlaylistHistoryEffects
{
    private const int MaxItems = 10;

    private readonly IHistoryRepository _history;

    public PlaylistHistoryEffects(IHistoryRepository history)
    {
        _history = history;
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
        dispatcher.Dispatch(new FetchPlaylistHistoryAlbumsResult(result));
    }

    [EffectMethod]
    public async Task HandleFetchPlaylistHistoryTagasRequest(FetchPlaylistHistoryTagsRequest action, IDispatcher dispatcher)
    {
        var result = await _history.GetRecentTags(MaxItems);
        dispatcher.Dispatch(new FetchPlaylistHistoryTagsResult(result));
    }
}
