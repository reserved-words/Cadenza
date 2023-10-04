using Cadenza.Common.Domain.Model;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlaylistHistoryAlbumsState(bool IsLoading, List<RecentAlbum> Items) 
{
    private static PlaylistHistoryAlbumsState Init() => new PlaylistHistoryAlbumsState(true, new List<RecentAlbum>());
}

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlaylistHistoryTagsState(bool IsLoading, List<string> Items)
{
    private static PlaylistHistoryTagsState Init() => new PlaylistHistoryTagsState(true, new List<string>());
}
