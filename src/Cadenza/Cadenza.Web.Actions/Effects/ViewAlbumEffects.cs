using Cadenza.Web.Common.Extensions;

namespace Cadenza.Web.Actions.Effects;

public class ViewAlbumEffects
{
    private readonly ILibraryApi _api;
    private readonly IState<ViewAlbumState> _state;

    public ViewAlbumEffects(ILibraryApi api, IState<ViewAlbumState> state)
    {
        _api = api;
        _state = state;
    }

    [EffectMethod]
    public async Task HandleFetchViewAlbumRequest(FetchViewAlbumRequest action, IDispatcher dispatcher)
    {
        var album = await _api.GetFullAlbum(action.AlbumId);
        dispatcher.Dispatch(new FetchViewAlbumResult(album));
    }

    [EffectMethod]
    public Task ReduceAlbumTracksUpdatedAction(AlbumTracksUpdatedAction action, IDispatcher dispatcher)
    {
        if (_state.Value?.Album?.Id != action.AlbumId)
            return Task.CompletedTask;

        dispatcher.Dispatch(new FetchViewAlbumRequest(action.AlbumId));
        return Task.CompletedTask;
    }
}
