using Cadenza.Web.Common.Extensions;

namespace Cadenza.Web.Actions.Effects;

public class ViewArtistEffects
{
    private readonly ILibraryApi _api;
    private readonly IState<ViewArtistState> _state;

    public ViewArtistEffects(ILibraryApi api, IState<ViewArtistState> state)
    {
        _api = api;
        _state = state;
    }

    [EffectMethod]
    public async Task HandleFetchViewArtistRequest(FetchViewArtistRequest action, IDispatcher dispatcher)
    {
        var artist = await _api.GetFullArtist(action.ArtistId, true);

        var releases = artist.Albums
            .GroupByReleaseType()
            .AddAlbumsFeaturingArtist(artist.AlbumsFeaturingArtist);

        dispatcher.Dispatch(new FetchViewArtistResult(artist.Artist, releases));
    }

    [EffectMethod]
    public Task HandleArtistReleasesUpdatedAction(ArtistReleasesUpdatedAction action, IDispatcher dispatcher)
    {
        if (_state.Value?.Artist?.Id != action.ArtistId)
            return Task.CompletedTask;

        dispatcher.Dispatch(new FetchViewArtistRequest(action.ArtistId));
        return Task.CompletedTask;
    }
}
