using TrackRemovalRequest = Cadenza.State.Actions.TrackRemovalRequest;

namespace Cadenza.Web.Components.Forms;

public class RemoveTrackBase : FormBase<TrackToRemove>
{
    [Inject] public IDispatcher Dispatcher { get; set; }

    protected void OnSubmit()
    {
        SubscribeToAction<TrackRemovedAction>(OnTrackRemoved);
        SubscribeToAction<TrackRemovalFailedAction>(OnTrackRemovalFailed);
        Dispatcher.Dispatch(new TrackRemovalRequest(Model.Id));
    }

    protected void OnCancel()
    {
        Cancel();
    }

    private void OnTrackRemoved(TrackRemovedAction action)
    {
        Submit();
    }
    private void OnTrackRemovalFailed(TrackRemovalFailedAction action)
    {
        Submit();
    }
}
