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
            IsNavigationDisabled = action.Tab == Tab.Settings
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
            ViewItem = new ViewItem(action.Type, action.Id, action.Name),
            IsNavigationDisabled = false
        };
    }

    [ReducerMethod]
    public static ViewState ReduceViewEditItemRequest(ViewState state, ViewEditItemRequest action)
    {
        if (state.IsNavigationDisabled)
            return state;

        var previousTab = state.CurrentTab;

        return state with
        {
            CurrentTab = Tab.Edit,
            PreviousTab = previousTab,
            EditItem = new EditItem(action.Type, action.Id, action.Name),
            IsNavigationDisabled = true
        };
    }

    [ReducerMethod]
    public static ViewState ReduceViewEditEndRequest(ViewState state, ViewResetRequest action)
    {
        return state with
        {
            CurrentTab = state.PreviousTab,
            EditItem = null,
            IsNavigationDisabled = false
        };
    }
}
