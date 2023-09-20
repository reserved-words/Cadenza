using Cadenza.Web.Common.Enums;
using Cadenza.Web.Common.Model;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewState(Tab Tab, ViewItem? Item)
{
    private static ViewState Init() => new ViewState(Tab.Home, null); 
}
