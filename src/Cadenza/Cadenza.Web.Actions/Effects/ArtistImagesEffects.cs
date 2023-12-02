namespace Cadenza.Web.Actions.Effects;

public class ArtistImagesEffects
{
    private readonly IState<ArtistImagesState> _state;
    private readonly IArtworkApi _api;

    public ArtistImagesEffects(IState<ArtistImagesState> state, IArtworkApi api)
    {
        _state = state;
        _api = api;
    }

    [EffectMethod]
    public Task HandleFetchArtistImageRequest(FetchArtistImageRequest action, IDispatcher dispatcher)
    {
        string result;

        if (action.ArtistId == 0)
        {
            result = _api.GetArtistImageUrl(null);
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
            result = _api.GetArtistImageUrl(action.ArtistId);
        }

        dispatcher.Dispatch(new FetchArtistImageResultAction(action.ArtistId, result));
        return Task.CompletedTask;
    }
}
