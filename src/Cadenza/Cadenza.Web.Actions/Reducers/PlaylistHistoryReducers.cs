using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Actions.Reducers;

public static class PlaylistHistoryReducers
{
    [ReducerMethod]
    public static PlaylistHistoryAlbumsState ReduceFetchPlaylistHistoryAlbumsRequest(PlaylistHistoryAlbumsState state, FetchPlaylistHistoryAlbumsRequest action) => state with
    {
        IsLoading = true,
        Items = new List<RecentAlbumVM>()
    };

    [ReducerMethod]
    public static PlaylistHistoryAlbumsState ReduceFetchPlaylistHistoryAlbumsResult(PlaylistHistoryAlbumsState state, FetchPlaylistHistoryAlbumsResult action) => state with
    {
        IsLoading = false,
        Items = action.Result
    };

    [ReducerMethod]
    public static PlaylistHistoryTagsState ReduceFetchPlaylistHistoryTagsRequest(PlaylistHistoryTagsState state, FetchPlaylistHistoryTagsRequest action) => state with
    {
        IsLoading = true,
        Items = new List<string>()
    };

    [ReducerMethod]
    public static PlaylistHistoryTagsState ReduceFetchPlaylistHistoryTagsResult(PlaylistHistoryTagsState state, FetchPlaylistHistoryTagsResult action) => state with
    {
        IsLoading = false,
        Items = action.Result
    };
}
