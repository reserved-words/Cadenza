namespace Cadenza.State.Actions.Effects;

public class AlbumArtworkEffects
{
    private readonly IState<AlbumArtworkState> _state;
    private readonly IArtworkFetcher _artworkFetcher;

    public AlbumArtworkEffects(IState<AlbumArtworkState> state, IArtworkFetcher artworkFetcher)
    {
        _state = state;
        _artworkFetcher = artworkFetcher;
    }

    [EffectMethod]
    public Task HandleFetchAlbumArtworkRequest(FetchAlbumArtworkRequest action, IDispatcher dispatcher)
    {
        string result;

        if (action.Album == null)
        {
            result = _artworkFetcher.GetAlbumArtworkSrc(null);
            dispatcher.Dispatch(new FetchAlbumArtworkResultAction(0, result));
            return Task.CompletedTask;
        }
        
        if (action.Album.ArtworkBase64 != null)
        {
            result = action.Album.ArtworkBase64;
        }
        else if (_state.Value.Images.TryGetValue(action.Album.Id, out string artwork))
        {
            result = artwork;
        }
        else
        {
            result = _artworkFetcher.GetAlbumArtworkSrc(action.Album.Id);
        }

        dispatcher.Dispatch(new FetchAlbumArtworkResultAction(action.Album.Id, result));
        return Task.CompletedTask;
    }
}
