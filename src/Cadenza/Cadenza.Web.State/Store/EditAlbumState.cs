using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record EditAlbumState(bool IsLoading, AlbumDetailsVM Album, IReadOnlyCollection<AlbumDiscVM> Tracks)
{
    private static EditAlbumState Init() => new EditAlbumState(true, null, null);
}
