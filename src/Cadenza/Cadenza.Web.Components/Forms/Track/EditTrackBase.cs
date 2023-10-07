namespace Cadenza.Web.Components.Forms.Track;

public class EditTrackBase : FormBase<TrackDetailsVM>
{
    [Inject] public IDispatcher Dispatcher { get; set; }

    public TrackUpdateVM Update { get; set; }
    public TrackDetailsVM EditableItem => Update.UpdatedItem;

    protected override void OnInitialized()
    {
        SubscribeToAction<TrackUpdatedAction>(OnTrackUpdated);
        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        Update = new TrackUpdateVM(Model);
    }

    protected void OnSubmit()
    {
        Update.ConfirmUpdates();

        if (!Update.Updates.Any())
        {
            Cancel();
            return;
        }

        Dispatcher.Dispatch(new TrackUpdateRequest(Model.Id, Update));
    }

    private void OnTrackUpdated(TrackUpdatedAction action)
    {
        Submit();
    }

    protected void OnCancel()
    {
        Cancel();
    }
}
