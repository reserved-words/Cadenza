using Cadenza.Common.Domain.Model;

namespace Cadenza.State.Reducers;

public static class PlaylistHistoryReducers
{
    [ReducerMethod]
    public static PlaylistHistoryAlbumsState ReduceFetchPlaylistHistoryAlbumsRequest(PlaylistHistoryAlbumsState state, FetchPlaylistHistoryAlbumsRequest action) => state with
    {
        IsLoading = true,
        Items = new List<RecentAlbum>()
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
