using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlaylistHistoryAlbumsState(bool IsLoading, IReadOnlyCollection<RecentAlbumVM> Items)
{
    private static PlaylistHistoryAlbumsState Init() => new PlaylistHistoryAlbumsState(true, new ReadOnlyCollection<RecentAlbumVM>(new List<RecentAlbumVM>()));
}
