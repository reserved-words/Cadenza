namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewState(Tab CurrentTab, Tab PreviousTab, ViewItem? Item, bool IsNavigationDisabled)
{
    private static ViewState Init() => new ViewState(Tab.Default, Tab.Default, null, false);
}
