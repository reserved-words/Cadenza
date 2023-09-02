namespace Cadenza.Web.Components.Forms;

public class EditTrackBase : FormBase<TrackInfo>
{
    [Inject]
    public INotificationService Alert { get; set; }

    [Inject]
    public IUpdateRepository UpdateRepository { get; set; }

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

            if (!Update.Updates.Any())
            {
                Cancel();
                return;
            }

            await UpdateRepository.UpdateTrack(Update);
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
}
