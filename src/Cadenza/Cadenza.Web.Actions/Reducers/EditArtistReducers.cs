namespace Cadenza.Web.Actions.Reducers;

public static class EditArtistReducers
{
    [ReducerMethod(typeof(FetchEditArtistRequest))]
    public static EditArtistState ReduceFetchEditArtistAction(EditArtistState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static EditArtistState ReduceFetchEditArtistResult(EditArtistState state, FetchEditArtistResult action) => state with
    {
        IsLoading = false,
        Artist = action.Artist,
        Releases = action.Releases
    };

    [ReducerMethod]
    public static EditArtistState ReduceViewEditEndRequest(EditArtistState state, ViewEditEndRequest action) => state with
    {
        IsLoading = false,
        Artist = null,
        Releases = null
    };
}
