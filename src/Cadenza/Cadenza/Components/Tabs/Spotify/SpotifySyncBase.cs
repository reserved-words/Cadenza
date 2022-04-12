using Cadenza.Interfaces;
using Cadenza.Utilities;
using IDialogService = Cadenza.Interfaces.IDialogService;

namespace Cadenza.Components.Tabs.Spotify;

public class SpotifySyncBase : ComponentBase
{
    [Inject]
    public IDialogService DialogService { get; set; }

    [Inject]
    public IHttpHelper HttpHelper { get; set; }

    [Inject]
    public IConfiguration Config { get; set; }

    [Parameter]
    public bool Loading { get; set; }

    [Parameter]
    public FullLibrary Library { get; set; }

    public DateTime? LastSyncDate { get; set; }

    public string SyncDateCaption => LastSyncDate.HasValue
        ? $"Library last synced on {LastSyncDate.Value.ToString("dd MMM yyyy")} at {LastSyncDate.Value.ToString("HH:mm:ss")}"
        : "No last library sync date available";

    protected override async Task OnInitializedAsync()
    {
        await PopulateSyncDate();
    }

    public async Task OnSync()
    {
        var baseUrl = Config.GetValue<string>("LocalApi:BaseUrl");
        var endpoint = Config.GetValue<string>("LocalApi:Endpoints:AddSource");
        var url = $"{baseUrl}{endpoint}";
        var sourceLibrary = new ExternalSourceLibrary
        {
            Source = LibrarySource.Spotify,
            Library = Library
        };
        await HttpHelper.Post(url, null, sourceLibrary);
        await PopulateSyncDate();
    }

    private async Task PopulateSyncDate()
    {
        var baseUrl = Config.GetValue<string>("LocalApi:BaseUrl");
        var endpoint = Config.GetValue<string>("LocalApi:Endpoints:SourceLastUpdated");
        var url = $"{baseUrl}{endpoint}{LibrarySource.Spotify}";
        var response = await HttpHelper.Get<SyncDateResponse>(url);
        LastSyncDate = response.SyncDate;
    }

    private class SyncDateResponse
    {
        public DateTime? SyncDate { get; set; }
    }
}
