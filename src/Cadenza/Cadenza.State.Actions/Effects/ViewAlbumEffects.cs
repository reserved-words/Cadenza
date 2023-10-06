using Cadenza.Web.Common.Extensions;

namespace Cadenza.State.Actions.Effects;

public class ViewAlbumEffects
{
    private readonly IAlbumRepository _repository;

    public ViewAlbumEffects(IAlbumRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleFetchViewAlbumRequest(FetchViewAlbumRequest action, IDispatcher dispatcher)
    {
        var album = await _repository.GetAlbum(action.AlbumId);
        var tracks = await _repository.GetAlbumTracks(action.AlbumId);
        var discs = tracks.GroupByDisc();
        dispatcher.Dispatch(new FetchViewAlbumResult(album, discs));
    }
}
