namespace Cadenza.Web.Actions.Effects;

public class AlbumArtworkEffects
{
    private readonly IState<AlbumArtworkState> _state;
    private readonly IArtworkApi _api;

    public AlbumArtworkEffects(IState<AlbumArtworkState> state, IArtworkApi api)
    {
        _state = state;
        _api = api;
    }

    [EffectMethod]
    public Task HandleFetchAlbumArtworkRequest(FetchAlbumArtworkRequest action, IDispatcher dispatcher)
    {
        string result;

        if (action.Id == 0)
        {
            result = _api.GetAlbumArtworkUrl(null);
            dispatcher.Dispatch(new FetchAlbumArtworkResultAction(0, result));
            return Task.CompletedTask;
        }

        if (action.ArtworkBase64 != null)
        {
            result = action.ArtworkBase64;
        }
        else if (_state.Value.Images.TryGetValue(action.Id, out string artwork))
        {
            result = artwork;
        }
        else
        {
            result = _api.GetAlbumArtworkUrl(action.Id);
        }

        dispatcher.Dispatch(new FetchAlbumArtworkResultAction(action.Id, result));
        return Task.CompletedTask;
    }
}
