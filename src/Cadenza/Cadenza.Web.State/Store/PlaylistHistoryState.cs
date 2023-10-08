namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlaylistHistoryAlbumsState(bool IsLoading, IReadOnlyCollection<RecentAlbumVM> Items)
{
    private static PlaylistHistoryAlbumsState Init() => new PlaylistHistoryAlbumsState(true, new ReadOnlyCollection<RecentAlbumVM>(new List<RecentAlbumVM>()));
}

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlaylistHistoryTagsState(bool IsLoading, IReadOnlyCollection<string> Items)
{
    private static PlaylistHistoryTagsState Init() => new PlaylistHistoryTagsState(true, new ReadOnlyCollection<string>(new List<string>()));
}
