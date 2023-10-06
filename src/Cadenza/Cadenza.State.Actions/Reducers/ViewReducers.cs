namespace Cadenza.State.Actions.Reducers;

public static class ViewReducers
{
    [ReducerMethod]
    public static ViewState ReduceViewTabRequest(ViewState state, ViewTabRequest action) => state with
    {
        Tab = action.Tab
    };

    [ReducerMethod]
    public static ViewState ReduceViewItemRequest(ViewState state, ViewItemRequest action) => state with
    {
        Tab = Tab.Library,
        Item = new ViewItem(action.Type, action.Id, action.Name)
    };
}
