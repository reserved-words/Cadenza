using Cadenza.Web.State.Actions;
using Cadenza.Web.State.Store;

namespace Cadenza.Web.Actions.Reducers;

public static class ViewGenreReducers
{
    [ReducerMethod(typeof(FetchViewGenreRequest))]
    public static ViewGenreState ReduceFetchViewGenreAction(ViewGenreState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static ViewGenreState ReduceFetchViewGenreResult(ViewGenreState state, FetchViewGenreResult action) => state with
    {
        IsLoading = false,
        Genre = action.Genre,
        Artists = action.Artists
    };
}
