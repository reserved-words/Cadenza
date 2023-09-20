namespace Cadenza.State.Reducers;

public static class PlayHistoryReducers
{
    [ReducerMethod]
    public static PlayHistoryAlbumsState ReduceFetchPlayHistoryAlbumsRequest(PlayHistoryAlbumsState state, FetchPlayHistoryAlbumsRequest action) => state with
    {
        IsLoading = true,
        Period = action.Period,
        Items = new List<PlayedAlbum>()
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
        Items = new List<PlayedArtist>()
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
        Items = new List<PlayedTrack>()
    };

    [ReducerMethod]
    public static PlayHistoryTracksState ReduceFetchPlayHistoryTracksResult(PlayHistoryTracksState state, FetchPlayHistoryTracksResult action) => state with
    {
        IsLoading = false,
        Period = action.Period,
        Items = action.Result
    };
}
