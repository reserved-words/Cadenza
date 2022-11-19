namespace Cadenza.Web.Components.Forms;

public class EditAlbumBase : FormBase<AlbumInfo>
{
    [Inject]
    public IImageFinder ImageFinder { get; set; }

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

    protected async Task OnLoad()
    {
        try
        {
            var artworkUrl = await ImageFinder.GetUrl(EditableItem);

            if (string.IsNullOrWhiteSpace(artworkUrl))
            {
                throw new Exception("No artwork found");
            }

            ArtworkUrl = artworkUrl;

            await OnUpdateUrl();
        }
        catch (Exception ex)
        {
            // Log error
            Alert.Error("Error loading artwork: " + ex.Message);
            Alert.Error("Error loading artwork: " + ex.StackTrace);
        }
    }

    protected async Task OnSearch()
    {
        var searchUrl = ImageFinder.GetSearchUrl(Model);
        await Navigation.OpenNewTab(searchUrl);
    }

    protected async Task OnSubmit()
    {
        try
        {
            Update.ConfirmUpdates();

            if (!Update.PropertyUpdates.Any())
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
            Alert.Error("Error updating album: " + ex.StackTrace);
        }
    }

    protected void OnCancel()
    {
        Cancel();
    }

    protected async Task OnUpdateUrl()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(ArtworkUrl))
            {
                throw new Exception("No URL entered");
            }

            EditableItem.ArtworkUrl = await ImageFinder.GetBase64ArtworkSource(ArtworkUrl);
        }
        catch (Exception ex)
        {
            Alert.Error(ex.Message);
            return;
        }
    }
}
