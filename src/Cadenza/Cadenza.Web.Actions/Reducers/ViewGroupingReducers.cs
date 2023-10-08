using Cadenza.Web.State.Actions;
using Cadenza.Web.State.Store;

namespace Cadenza.Web.Actions.Reducers;

public static class ViewGroupingReducers
{
    [ReducerMethod(typeof(FetchViewGroupingRequest))]
    public static ViewGroupingState ReduceFetchViewGroupingAction(ViewGroupingState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static ViewGroupingState ReduceFetchViewGroupingResult(ViewGroupingState state, FetchViewGroupingResult action) => state with
    {
        IsLoading = false,
        Grouping = action.Grouping,
        Genres = action.Genres
    };
}
