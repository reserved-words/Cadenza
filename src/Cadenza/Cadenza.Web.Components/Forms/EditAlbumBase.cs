namespace Cadenza.Web.Components.Forms;

public class EditAlbumBase : FormBase<AlbumInfo>
{
    [Inject]
    public INotificationService Alert { get; set; }

    [Inject]
    public IUpdateService UpdateService { get; set; }

    [Inject]
    public IUpdatesCoordinator UpdatesCoordinator { get; set; }

    public AlbumUpdate Update { get; set; }

    public AlbumInfo EditableItem => Update.UpdatedItem;

    protected override void OnParametersSet()
    {
        Update = new AlbumUpdate(Model);
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
}
