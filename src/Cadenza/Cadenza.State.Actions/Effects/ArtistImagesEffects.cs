namespace Cadenza.State.Actions.Effects;

public class ArtistImagesEffects
{
    private readonly IState<ArtistImagesState> _state;
    private readonly IArtworkFetcher _artworkFetcher;

    public ArtistImagesEffects(IState<ArtistImagesState> state, IArtworkFetcher artworkFetcher)
    {
        _state = state;
        _artworkFetcher = artworkFetcher;
    }

    [EffectMethod]
    public Task HandleFetchArtistImageRequest(FetchArtistImageRequest action, IDispatcher dispatcher)
    {
        string result;

        if (action.ArtistId == 0)
        {
            result = _artworkFetcher.GetArtistImageSrc(null);
            dispatcher.Dispatch(new FetchArtistImageResultAction(0, result));
            return Task.CompletedTask;
        }
        
        if (action.ImageBase64 != null)
        {
            result = action.ImageBase64;
        }
        else if (_state.Value.Images.TryGetValue(action.ArtistId, out string image))
        {
            result = image;
        }
        else
        {
            result = _artworkFetcher.GetArtistImageSrc(action.ArtistId);
        }

        dispatcher.Dispatch(new FetchArtistImageResultAction(action.ArtistId, result));
        return Task.CompletedTask;
    }
}
