using Cadenza.Web.State.Actions;
using Cadenza.Web.State.Store;

namespace Cadenza.Web.Actions.Reducers;

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
