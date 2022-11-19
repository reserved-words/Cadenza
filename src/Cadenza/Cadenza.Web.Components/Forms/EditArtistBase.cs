namespace Cadenza.Web.Components.Forms;

public class EditArtistBase : FormBase<ArtistInfo>
{
    [Inject]
    public IImageFinder ImageFinder { get; set; }

    [Inject]
    public INavigation Navigation { get; set; }

    [Inject]
    public IUpdateService Repository { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    [Inject]
    public IUpdatesCoordinator UpdatesCoordinator { get; set; }

    public string ImageUrl { get; set; }

    public ArtistUpdate Update { get; set; }

    public ArtistInfo EditableItem => Update.UpdatedItem;

    protected override void OnParametersSet()
    {
        Update = new ArtistUpdate(Model);
    }

    protected async Task OnSearch(SearchSource source)
    {
        var searchUrl = ImageFinder.GetSearchUrl(Model, source);
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

            await Repository.UpdateArtist(Update);
            Alert.Success("Artist updated");
            await UpdatesCoordinator.UpdateArtist(Update);
            Submit();
        }
        catch (Exception ex)
        {
            // Log error
            Alert.Error("Error updating artist: " + ex.Message);
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
            if (string.IsNullOrWhiteSpace(ImageUrl))
            {
                throw new Exception("No URL entered");
            }

            EditableItem.ImageUrl = await ImageFinder.GetBase64ArtworkSource(ImageUrl);
        }
        catch (Exception ex)
        {
            Alert.Error(ex.Message);
            return;
        }
    }
}
