namespace Cadenza.Web.Actions.Effects;

public class ViewAlbumEffects
{
    private readonly IAlbumRepository _repository;
    private readonly IArtworkFetcher _artworkFetcher;

    public ViewAlbumEffects(IAlbumRepository repository, IArtworkFetcher artworkFetcher)
    {
        _repository = repository;
        _artworkFetcher = artworkFetcher;
    }

    [EffectMethod]
    public async Task HandleFetchViewAlbumRequest(FetchViewAlbumRequest action, IDispatcher dispatcher)
    {
        var album = await _repository.GetAlbum(action.AlbumId);
        album.ImageUrl = _artworkFetcher.GetAlbumArtworkSrc(album.Id);
        var tracks = await _repository.GetAlbumTracks(action.AlbumId);
        dispatcher.Dispatch(new FetchViewAlbumResult(album, tracks.Discs));
    }
}
