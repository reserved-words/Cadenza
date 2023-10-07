namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record EditableAlbumState(bool IsLoading, List<AlbumTrackVM> Tracks)
{
    private static EditableAlbumState Init() => new EditableAlbumState(false, new List<AlbumTrackVM>());
}
