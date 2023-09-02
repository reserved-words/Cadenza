namespace Cadenza.Web.Components.Forms;

public class EditArtistBase : FormBase<ArtistInfo>
{
    [Inject]
    public IAdminRepository AdminRepository { get; set; }

    [Inject]
    public IUpdateRepository UpdateRepository { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    [Inject]
    public IUpdatesCoordinator UpdatesCoordinator { get; set; }

    public ArtistUpdate Update { get; set; }

    public List<Grouping> Groupings { get; set; } = new List<Grouping>();

    public ArtistInfo EditableItem => Update.UpdatedItem;

    protected override async Task OnInitializedAsync()
    {
        Groupings = await AdminRepository.GetGroupingOptions();
    }

    protected override void OnParametersSet()
    {
        Update = new ArtistUpdate(Model);
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

            await UpdateRepository.UpdateArtist(Update);
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
}
