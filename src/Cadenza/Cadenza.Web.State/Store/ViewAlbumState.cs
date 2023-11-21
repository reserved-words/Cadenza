namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewAlbumState(bool IsLoading, AlbumDetailsVM Album, IReadOnlyCollection<AlbumDiscVM> Tracks)
{
    private static ViewAlbumState Init() => new ViewAlbumState(true, null, null);
}