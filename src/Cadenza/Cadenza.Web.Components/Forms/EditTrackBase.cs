namespace Cadenza.Web.Components.Forms;

public class EditTrackBase : FormBase<TrackInfo>
{
    private const string SearchUrl = "https://www.google.com/search?q=%22{0}%22+%22{1}%22+lyrics";

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

    public TrackUpdate Update { get; set; }

    public TrackInfo EditableItem => Update.UpdatedItem;

    protected override void OnParametersSet()
    {
        Update = new TrackUpdate(Model);
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

            await UpdateService.UpdateTrack(Update);
            Alert.Success("Track updated");
            await UpdatesCoordinator.UpdateTrack(Update);
            Submit();
        }
        catch (Exception ex)
        {
            // Log error
            Alert.Error("Error updating track: " + ex.Message);
            Alert.Error("Error updating track: " + ex.StackTrace);
        }
    }

    protected void OnCancel()
    {
        Cancel();
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

    private string GetSearchUrl()
    {
        var artist = HttpUtility.UrlEncode(Model.ArtistName);
        var title = HttpUtility.UrlEncode(Model.Title);
        return string.Format(SearchUrl, artist, title);
    }

    private static bool AreEqual(string originalValue, string updatedValue)
    {
        if (originalValue == null && updatedValue == null)
            return true;

        if (originalValue == null || updatedValue == null)
            return false;

        return originalValue == updatedValue;
    }
}
