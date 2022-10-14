using Cadenza.Web.Common.Interfaces.Updates;

namespace Cadenza.Web.Components.Shared.Dialogs;

public class EditArtworkBase : FormBase<AlbumInfo>
{
    private const string SearchUrl = "https://www.google.com/search?tbm=isch&q=%22{0}%22+%22{1}%22";

    [Inject]
    public IUpdateService Repository { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    [Inject]
    public IUpdatesCoordinator UpdatesService { get; set; }

    [Inject]
    public INavigation Navigation { get; set; }

    public AlbumUpdate Update { get; set; }

    public AlbumInfo EditableItem => Update.UpdatedItem;

    public List<DialogAction> Actions { get; set; } = new();

    public string ArtworkUrl { get; set; }

    protected override void OnInitialized()
    {
        Actions = new List<DialogAction>
        {
            new DialogAction("Load", OnLoad),
            new DialogAction("Search", OnSearch)
        };
    }

    protected override void OnParametersSet()
    {
        Update = new AlbumUpdate(Model);
    }

    protected Task OnLoad()
    {
        return Task.CompletedTask;
    }

    protected async Task OnSearch()
    {
        var searchUrl = GetSearchUrl();
        await Navigation.OpenNewTab(searchUrl);
    }

    protected async Task OnSubmit()
    {
        try
        {
            Update.ConfirmUpdates();

            if (!Update.Updates.Any())
            {
                Cancel();
                return;
            }

            Alert.Error("NOT IMPLEMENTED YET - STILL TESTING");

            //await Repository.UpdateAlbum(Update);
            //Alert.Success("Artwork updated");
        }
        catch (Exception ex)
        {
            // Log error
            Alert.Error("Error updating artwork: " + ex.Message);
        }

        await UpdatesService.UpdateArtwork(Update);
        Submit();
    }

    protected void OnCancel()
    {
        Cancel();
    }

    private string GetSearchUrl()
    {
        var artist = HttpUtility.UrlEncode(Model.ArtistName);
        var title = HttpUtility.UrlEncode(Model.Title);
        return string.Format(SearchUrl, artist, title);
    }

    protected Task OnUpdateUrl()
    {
        if (string.IsNullOrWhiteSpace(ArtworkUrl))
        {
            Alert.Error("No URL entered");
            return Task.CompletedTask;
        }

        if (!Uri.TryCreate(ArtworkUrl, UriKind.Absolute, out Uri uri))
        {
            Alert.Error("URL is invalid");
            return Task.CompletedTask;
        }

        EditableItem.ArtworkUrl = ArtworkUrl;
        return Task.CompletedTask;
    }
}
