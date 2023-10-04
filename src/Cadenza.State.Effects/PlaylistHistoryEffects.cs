namespace Cadenza.State.Effects;

public class PlaylistHistoryEffects
{
    private const int MaxItems = 10;

    private readonly IPlaylistHistory _history;

    public PlaylistHistoryEffects(IPlaylistHistory history)
    {
        _history = history;
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
