namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record EditableAlbumState(bool IsLoading, IReadOnlyCollection<AlbumTrackVM> Tracks)
{
    private static EditableAlbumState Init() => new EditableAlbumState(false, new ReadOnlyCollection<AlbumTrackVM>(new List<AlbumTrackVM>()));
}
