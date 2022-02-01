using Cadenza.LastFM;
using Microsoft.AspNetCore.WebUtilities;

namespace Cadenza;

public class LastFmConnectBase : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IAppController App { get; set; }

    [Inject]
    public IConfiguration Config { get; set; }

    [Inject]
    public ILastFmStartup StartupService { get; set; }

    private Uri CurrentUri => NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

    private string RedirectUri => Config.GetSection("LastFm").GetValue<string>("RedirectUri");

    protected override async Task OnParametersSetAsync()
    {
        if (QueryHelpers.ParseQuery(CurrentUri.Query).TryGetValue("token", out var token))
        {
            await CreateSession(token);
            return;
        }

        var sessionKey = await StartupService.GetSessionKey();

        if (sessionKey == null)
        {
            var authUrl = await StartupService.GetAuthUrl(RedirectUri);
            NavigationManager.NavigateTo(authUrl);
            return;
        }

        // await App.EnableConnector(Connector.LastFm);
    }

    private async Task CreateSession(string token)
    {
        var connected = await StartupService.CreateSession(token);

        if (connected)
        {
            // await App.EnableConnector(Connector.LastFm);
        }
        else
        {
            // await App.DisableConnector(Connector.LastFm, ConnectorError.ConnectFailure, "Failed to create session");
        }
    }
}

