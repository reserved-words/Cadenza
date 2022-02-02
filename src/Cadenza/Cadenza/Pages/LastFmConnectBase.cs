using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;

namespace Cadenza;

public class LastFmConnectBase : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IStoreSetter Store { get; set; }

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    private Uri CurrentUri => NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

    protected override async Task OnParametersSetAsync()
    {
        if (QueryHelpers.ParseQuery(CurrentUri.Query).TryGetValue("token", out var token))
        {
            await Store.SetValue<string>(StoreKey.LastFmToken, token);
        }

        // Maybe display a message instead saying to go back to main window
        await CloseTab();
    }

    public async Task CloseTab()
    {
        // Doesn't work in Chrome but not a huge problem for now
        await JSRuntime.InvokeAsync<object>("close");
    }
}

