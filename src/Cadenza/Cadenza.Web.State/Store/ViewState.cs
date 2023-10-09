namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewState(Tab Tab, ViewItem? Item)
{
    private static ViewState Init() => new ViewState(Tab.Home, null);
}
