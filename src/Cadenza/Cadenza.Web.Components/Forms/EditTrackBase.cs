using Cadenza.Common.Domain.Model.Library;
using Cadenza.Common.Domain.Model.Updates;
using Cadenza.State.Actions;
using Fluxor;

namespace Cadenza.Web.Components.Forms;

public class EditTrackBase : FormBase<Cadenza.Common.Domain.Model.Library.TrackDetails>
{
    [Inject] public IDispatcher Dispatcher { get; set; }

    public TrackUpdate Update { get; set; }
    public Cadenza.Common.Domain.Model.Library.TrackDetails EditableItem => Update.UpdatedItem;

    protected override void OnInitialized()
    {
        SubscribeToAction<TrackUpdatedAction>(OnTrackUpdated);
        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        Update = new TrackUpdate(Model);
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
