using Cadenza.Common;

namespace Cadenza.Components.Sidebar;

public class SearchResultBase : ComponentBase
{
    [Parameter]
    public SearchableItem Result { get; set; }
}

