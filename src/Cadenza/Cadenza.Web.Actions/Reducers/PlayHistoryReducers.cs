using Cadenza.Web.State.Actions;
using Cadenza.Web.State.Store;

namespace Cadenza.Web.Actions.Reducers;

public static class PlayHistoryReducers
{
    [ReducerMethod]
    public static PlayHistoryAlbumsState ReduceFetchPlayHistoryAlbumsRequest(PlayHistoryAlbumsState state, FetchPlayHistoryAlbumsRequest action) => state with
    {
        IsLoading = true,
        Period = action.Period,
        Items = new List<PlayedAlbumVM>()
    };

    [ReducerMethod]
    public static PlayHistoryAlbumsState ReduceFetchPlayHistoryAlbumsResult(PlayHistoryAlbumsState state, FetchPlayHistoryAlbumsResult action) => state with
    {
        IsLoading = false,
        Period = action.Period,
        Items = action.Result
    };

    [ReducerMethod]
    public static PlayHistoryArtistsState ReduceFetchPlayHistoryArtistsRequest(PlayHistoryArtistsState state, FetchPlayHistoryArtistsRequest action) => state with
    {
        IsLoading = true,
        Period = action.Period,
        Items = new List<PlayedArtistVM>()
    };

    [ReducerMethod]
    public static PlayHistoryArtistsState ReduceFetchPlayHistoryArtistsResult(PlayHistoryArtistsState state, FetchPlayHistoryArtistsResult action) => state with
    {
        IsLoading = false,
        Period = action.Period,
        Items = action.Result
    };

    [ReducerMethod]
    public static PlayHistoryTracksState ReduceFetchPlayHistoryTracksRequest(PlayHistoryTracksState state, FetchPlayHistoryTracksRequest action) => state with
    {
        IsLoading = true,
        Period = action.Period,
        Items = new List<PlayedTrackVM>()
    };

    [ReducerMethod]
    public static PlayHistoryTracksState ReduceFetchPlayHistoryTracksResult(PlayHistoryTracksState state, FetchPlayHistoryTracksResult action) => state with
    {
        IsLoading = false,
        Period = action.Period,
        Items = action.Result
    };
}
