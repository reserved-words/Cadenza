using Cadenza.Common;
using Cadenza.Source.Spotify;
using Microsoft.AspNetCore.WebUtilities;

namespace Cadenza;

public class SpotifyConnectBase : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IAppController App { get; set; }

    [Inject]
    public ISpotifyStartup StartupService { get; set; }

    [Inject]
    public IConfiguration Config { get; set; }

    private Uri CurrentUri => NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

    private string RedirectUri => Config.GetSection("Spotify").GetValue<string>("RedirectUri");

    protected override async Task OnParametersSetAsync()
    {
        if (QueryHelpers.ParseQuery(CurrentUri.Query).TryGetValue("code", out var code))
        {
            await StartupService.StartSession(code, RedirectUri);
            NavigationManager.NavigateTo("/");
            return;
        }

        var accessToken = await StartupService.GetAccessToken();

        if (accessToken == null)
        {
            var authUrl = await StartupService.GetAuthUrl(RedirectUri);
            NavigationManager.NavigateTo(authUrl);
            return;
        }

        await InitialisePlayer(accessToken);
    }

    private async Task InitialisePlayer(string accessToken)
    {
        var connected = await StartupService.InitialisePlayer(accessToken);

        if (connected)
        {
            await App.EnableConnector(Connector.Spotify);
        }
        else
        {
            await App.DisableConnector(Connector.Spotify, ConnectorError.ConnectFailure, "Failed to connect");
        }
    }
}