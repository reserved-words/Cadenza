namespace Cadenza.Web.Actions.Effects;

public class ViewTrackEffects
{
    private readonly ITrackRepository _repository;
    private readonly IArtworkFetcher _artworkFetcher;

    public ViewTrackEffects(ITrackRepository repository, IArtworkFetcher artworkFetcher)
    {
        _repository = repository;
        _artworkFetcher = artworkFetcher;
    }

    [EffectMethod]
    public async Task HandleFetchViewTrackRequest(FetchViewTrackRequest action, IDispatcher dispatcher)
    {
        var track = await _repository.GetTrack(action.TrackId);
        track.Album.ImageUrl = _artworkFetcher.GetAlbumArtworkSrc(track.Album.Id);
        dispatcher.Dispatch(new FetchViewTrackResult(track));
    }
}
