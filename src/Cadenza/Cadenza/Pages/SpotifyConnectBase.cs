using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;

namespace Cadenza;

public class SpotifyConnectBase : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IStoreGetter StoreGetter { get; set; }

    [Inject]
    public IStoreSetter StoreSetter { get; set; }

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    private Uri CurrentUri => NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

    protected override async Task OnParametersSetAsync()
    {
        var code = QueryHelpers.ParseQuery(CurrentUri.Query).GetValueOrDefault("code").SingleOrDefault();
        var state = QueryHelpers.ParseQuery(CurrentUri.Query).GetValueOrDefault("state").SingleOrDefault();

        var storedState = await StoreGetter.GetValue<string>(StoreKey.SpotifyState);

        if (storedState.Value == state)
        {
            await StoreSetter.SetValue(StoreKey.SpotifyCode, code);
        }

        await StoreSetter.Clear(StoreKey.SpotifyState);

        await CloseTab();
    }

    public async Task CloseTab()
    {
        await JSRuntime.InvokeAsync<object>("close");
    }

    // TODO: Add state to request and check it in the response

    //If the user does not accept your request or if an error has occurred, the response query string contains the following parameters:

    //QUERY PARAMETER VALUE
    //error   The reason authorization failed, for example: “access_denied”
    //state The value of the state parameter supplied in the request.
}