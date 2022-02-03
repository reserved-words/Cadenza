using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;

namespace Cadenza;

public class SpotifyConnectBase : ComponentBase
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
        if (QueryHelpers.ParseQuery(CurrentUri.Query).TryGetValue("code", out var token))
        {
            await Store.SetValue(StoreKey.SpotifyCode, token.SingleOrDefault());
        }

        await CloseTab();
    }

    public async Task CloseTab()
    {
        await JSRuntime.InvokeAsync<object>("close");
    }
}