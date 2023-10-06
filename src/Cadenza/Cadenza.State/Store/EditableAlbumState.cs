using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record EditableAlbumState(bool IsLoading, List<AlbumTrack> Tracks)
{
    private static EditableAlbumState Init() => new EditableAlbumState(false, new List<AlbumTrack>());
}
