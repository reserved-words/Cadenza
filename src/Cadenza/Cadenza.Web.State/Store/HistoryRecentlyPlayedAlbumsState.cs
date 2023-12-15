using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record HistoryRecentlyPlayedAlbumsState(bool IsLoading, IReadOnlyCollection<RecentAlbumVM> Items)
{
    private static HistoryRecentlyPlayedAlbumsState Init() => new HistoryRecentlyPlayedAlbumsState(true, new ReadOnlyCollection<RecentAlbumVM>(new List<RecentAlbumVM>()));
}
