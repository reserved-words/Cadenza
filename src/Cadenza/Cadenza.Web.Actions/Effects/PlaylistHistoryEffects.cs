﻿namespace Cadenza.Web.Actions.Effects;

public class PlaylistHistoryEffects
{
    private const int MaxItems = 12;

    private readonly IArtworkApi _artworkApi;
    private readonly IHistoryApi _historyApi;

    public PlaylistHistoryEffects(IHistoryApi historyApi, IArtworkApi artworkApi)
    {
        _historyApi = historyApi;
        _artworkApi = artworkApi;
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
        var result = await _historyApi.GetRecentAlbums(MaxItems);
        
        var resultWithImages = result.Select(album => album with
        {
            ImageUrl = _artworkApi.GetAlbumArtworkUrl(album.Id)
        })
        .ToList();

        dispatcher.Dispatch(new FetchPlaylistHistoryAlbumsResult(resultWithImages));
    }

    [EffectMethod]
    public async Task HandleFetchPlaylistHistoryTagasRequest(FetchPlaylistHistoryTagsRequest action, IDispatcher dispatcher)
    {
        var result = await _historyApi.GetRecentTags(MaxItems);
        dispatcher.Dispatch(new FetchPlaylistHistoryTagsResult(result));
    }
}
