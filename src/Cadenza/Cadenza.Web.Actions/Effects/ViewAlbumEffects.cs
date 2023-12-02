﻿namespace Cadenza.Web.Actions.Effects;

public class ViewAlbumEffects
{
    private readonly ILibraryApi _api;

    public ViewAlbumEffects(ILibraryApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFetchViewAlbumRequest(FetchViewAlbumRequest action, IDispatcher dispatcher)
    {
        var album = await _api.GetAlbum(action.AlbumId);
        var tracks = await _api.GetAlbumTracks(action.AlbumId);
        dispatcher.Dispatch(new FetchViewAlbumResult(album, tracks.Discs));
    }
}
