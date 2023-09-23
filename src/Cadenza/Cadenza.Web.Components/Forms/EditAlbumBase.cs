using Cadenza.State.Actions;
using Fluxor;

namespace Cadenza.Web.Components.Forms;

public class EditAlbumBase : FormBase<AlbumInfo>
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    
    public AlbumUpdate Update { get; set; }
    public AlbumInfo EditableItem => Update.UpdatedItem;

    protected override void OnInitialized()
    {
        SubscribeToAction<AlbumUpdatedAction>(OnAlbumUpdated);
        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        Update = new AlbumUpdate(Model);
    }

    protected void OnSubmit()
    {
        Update.ConfirmUpdates();

        if (!Update.Updates.Any())
        {
            Cancel();
            return;
        }

        Dispatcher.Dispatch(new AlbumUpdateRequest(Model.Id, Update));
    }

    private void OnAlbumUpdated(AlbumUpdatedAction action)
    {
        Submit();
    }

    protected void OnCancel()
    {
        Cancel();
    }
}
