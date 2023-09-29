namespace Cadenza.State.Reducers;

public static class ViewTagReducers
{
    [ReducerMethod(typeof(FetchViewTagRequest))]
    public static ViewTagState ReduceFetchViewTagAction(ViewTagState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static ViewTagState ReduceFetchViewTagResult(ViewTagState state, FetchViewTagResult action) => state with
    {
        IsLoading = false,
        Tag = action.Tag,
        Items = action.Items
    };
}
