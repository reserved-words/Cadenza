using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record RecentlyAddedAlbumsState(bool IsLoading, IReadOnlyCollection<RecentAlbumVM> Albums)
{
    private static RecentlyAddedAlbumsState Init() => new RecentlyAddedAlbumsState(true, null);
}
