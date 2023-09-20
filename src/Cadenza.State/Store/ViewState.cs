using Cadenza.Web.Common.Enums;
using Cadenza.Web.Common.Model;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewState(Tab Tab, ViewItem? Item)
{
    // TODO: defaulting to History is fine for desktop view but should be Home for mobile / tablet view
    private static ViewState Init() => new ViewState(Tab.History, null); 
}
