using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record EditAlbumState(bool IsLoading, UpdateAlbumVM Album, IReadOnlyCollection<UpdateAlbumTrackVM> Tracks)
{
    private static EditAlbumState Init() => new EditAlbumState(true, null, null);
}
