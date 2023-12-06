namespace Cadenza.Web.Actions.Reducers;

public static class ViewReducers
{
    [ReducerMethod]
    public static ViewState ReduceViewTabRequest(ViewState state, ViewTabRequest action) 
    {
        if (state.IsNavigationDisabled)
            return state;

        var previousTab = state.CurrentTab == action.Tab
            ? state.PreviousTab
            : state.CurrentTab;

        return state with
        {
            CurrentTab = action.Tab,
            PreviousTab = previousTab,
            IsNavigationDisabled = action.Tab == Tab.Edit
        };
    }

    [ReducerMethod]
    public static ViewState ReduceViewItemRequest(ViewState state, ViewItemRequest action) 
    {
        if (state.IsNavigationDisabled)
            return state;

        var previousTab = state.CurrentTab == Tab.Library
            ? state.PreviousTab
            : state.CurrentTab;

        return state with
        {
            CurrentTab = Tab.Library,
            PreviousTab = previousTab,
            Item = new ViewItem(action.Type, action.Id, action.Name),
            IsNavigationDisabled = false
        };
    }

    [ReducerMethod]
    public static ViewState ReduceViewResetRequest(ViewState state, ViewResetRequest action)
    {
        var previousTab = state.PreviousTab;

        return state with
        {
            CurrentTab = previousTab,
            PreviousTab = previousTab,
            IsNavigationDisabled = false
        };
    }
}
