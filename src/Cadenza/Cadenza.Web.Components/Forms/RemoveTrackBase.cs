using Cadenza.Web.Common.Model;

namespace Cadenza.Web.Components.Forms;

public class RemoveTrackBase : FormBase<TrackToRemove>
{
    [Inject]
    public INotificationService Alert { get; set; }

    [Inject]
    public IUpdateRepository UpdateRepository { get; set; }

    [Inject]
    public IUpdatesCoordinator UpdatesCoordinator { get; set; }

    protected async Task OnSubmit()
    {
        try
        {
            await UpdateRepository.RemoveTrack(Model.Id);
            Alert.Success("Track removed");
            await UpdatesCoordinator.RemoveTrack(Model.Id);
            Submit();
        }
        catch (Exception ex)
        {
            // Log error
            Alert.Error("Error removing track: " + ex.Message);
            Alert.Error("Error removing track: " + ex.StackTrace);
        }
    }

    protected void OnCancel()
    {
        Cancel();
    }
}
