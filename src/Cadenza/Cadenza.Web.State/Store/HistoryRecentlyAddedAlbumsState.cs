using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record HistoryRecentlyAddedAlbumsState(bool IsLoading, IReadOnlyCollection<RecentAlbumVM> Albums)
{
    private static HistoryRecentlyAddedAlbumsState Init() => new HistoryRecentlyAddedAlbumsState(true, null);
}