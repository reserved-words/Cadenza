using Cadenza.Web.Common.Interfaces.Updates;

namespace Cadenza.Web.Components.Shared.Dialogs;

public class EditAlbumBase : FormBase<AlbumInfo>
{
    private const string ArtworkSearchUrl = "https://www.google.com/search?tbm=isch&q=%22{0}%22+%22{1}%22";

    [Inject]
    public INavigation Navigation { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    [Inject]
    public IUpdateService UpdateService { get; set; }

    [Inject]
    public IUpdatesCoordinator UpdatesCoordinator { get; set; }

    public string ArtworkUrl { get; set; }

    public AlbumUpdate Update { get; set; }

    public AlbumInfo EditableItem => Update.UpdatedItem;

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

            await UpdateService.UpdateAlbum(Update);
            Alert.Success("Album updated");
            await UpdatesCoordinator.UpdateAlbum(Update);
            Submit();
        }
        catch (Exception ex)
        {
            // Log error
            Alert.Error("Error updating album: " + ex.Message);
        }
    }

    protected void OnCancel()
    {
        Cancel();
    }

    private string GetSearchUrl()
    {
        var artist = HttpUtility.UrlEncode(Model.ArtistName);
        var title = HttpUtility.UrlEncode(Model.Title);
        return string.Format(ArtworkSearchUrl, artist, title);
    }

    protected Task OnUpdateUrl()
    {
        // TODO
        // Check it's actually an image file

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
