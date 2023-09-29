namespace Cadenza.State.Reducers;

public static class SearchItemsReducers
{
    [ReducerMethod(typeof(SearchItemsUpdateRequest))]
    public static SearchItemsState ReduceSearchItemsUpdateRequest(SearchItemsState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static SearchItemsState ReduceSearchItemsUpdatedAction(SearchItemsState state, SearchItemsUpdatedAction action) => state with
    {
        IsLoading = false,
        Items = action.Result
    };
}
