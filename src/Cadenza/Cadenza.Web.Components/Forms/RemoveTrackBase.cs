using Cadenza.State.Actions;
using Cadenza.State.Store;
using Cadenza.Web.Common.Model;
using Fluxor;

namespace Cadenza.Web.Components.Forms;

public class RemoveTrackBase : FormBase<TrackToRemove>
{
    [Inject] public INotificationService Alert { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<TrackRemovalState> TrackRemovalState { get; set; }

    protected void OnSubmit()
    {
        TrackRemovalState.StateChanged += TrackRemovalState_StateChanged;
        Dispatcher.Dispatch(new TrackRemovalRequest(Model.Id));
    }

    protected void OnCancel()
    {
        Cancel();
    }

    private void TrackRemovalState_StateChanged(object sender, EventArgs e)
    {
        // TODO: Move alert notification stuff and logging into effects
        if (TrackRemovalState.Value.IsLoading)
            return;

        TrackRemovalState.StateChanged -= TrackRemovalState_StateChanged;

        if (TrackRemovalState.Value.Error != null)
        {
            // Log error
            Alert.Error("Error removing track: " + TrackRemovalState.Value.Error);
        }
        else
        {
            Alert.Success("Track removed");
            Submit();
        }
    }
}
