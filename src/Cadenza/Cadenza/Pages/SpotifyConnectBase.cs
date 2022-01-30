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

    protected override async Task OnInitializedAsync()
    {
        if (QueryHelpers.ParseQuery(CurrentUri.Query).TryGetValue("code", out var code))
        {
            await Connect(code);
            return;
        }

        var accessToken = await StartupService.GetAccessToken();

        if (accessToken == null)
        {
            await Authorise();
            return;
        }

        await InitialisePlayer(accessToken);
    }

    private async Task Authorise()
    {
        var authUrl = await StartupService.GetAuthUrl(RedirectUri);
        NavigationManager.NavigateTo(authUrl);
    }

    private async Task Connect(string code)
    {
        await StartupService.ConnectToApi(code, RedirectUri);
        NavigationManager.NavigateTo("/");
    }

    private async Task InitialisePlayer(string accessToken)
    {
        var connected = await StartupService.InitialisePlayer(accessToken);

        if (connected)
        {
            await App.EnableSource(LibrarySource.Spotify);
        }
        else
        {
            await App.DisableSource(new SourceException(LibrarySource.Spotify, SourceError.ConnectFailure, "Failed to connect"));
        }
    }
}