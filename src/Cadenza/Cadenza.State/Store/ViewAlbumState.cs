namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewAlbumState(bool IsLoading, AlbumDetailsVM Album, IReadOnlyCollection<DiscVM> Discs) 
{
    private static ViewAlbumState Init() => new ViewAlbumState(true, null, null);
}