namespace Cadenza.Web.Actions.Effects;

public class CurrentTrackEffects
{
    private readonly IArtworkFetcher _artworkFetcher;
    private readonly ITrackRepository _repository;

    public CurrentTrackEffects(ITrackRepository repository, IArtworkFetcher artworkFetcher)
    {
        _repository = repository;
        _artworkFetcher = artworkFetcher;
    }

    [EffectMethod]
    public async Task HandleFetchTrackRequest(FetchTrackRequest action, IDispatcher dispatcher)
    {
        var fullTrack = action.TrackId == 0
            ? null
            : await _repository.GetTrack(action.TrackId);

        var isLast = fullTrack == null
            ? false
            : action.IsLastTrackInPlaylist;

        if (fullTrack != null)
        {
            fullTrack.Album.ImageUrl = _artworkFetcher.GetAlbumArtworkSrc(fullTrack.Album.Id);
        }

        dispatcher.Dispatch(new UpdateCurrentTrackAction(action.TrackId, fullTrack, isLast));
    }

    [EffectMethod]
    public Task HandleUpdateCurrentTrackAction(UpdateCurrentTrackAction action, IDispatcher dispatcher)
    {
        if (action.FullTrack != null)
        {
            dispatcher.Dispatch(new PlayerPlayRequest(action.FullTrack));
        }
        return Task.CompletedTask;
    }
}
