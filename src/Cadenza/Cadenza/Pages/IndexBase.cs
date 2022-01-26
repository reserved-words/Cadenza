using Cadenza.Core;

namespace Cadenza;

public class IndexBase : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IStoreSetter Store { get; set; }

    public bool ClientSideStartUpDone => NavigationManager.Uri.Contains("/player");

    protected override async Task OnInitializedAsync()
    {
        await Store.SetValue(StoreKey.CurrentTrackSource, null);
        await Store.SetValue(StoreKey.CurrentTrack, null);
    }
}