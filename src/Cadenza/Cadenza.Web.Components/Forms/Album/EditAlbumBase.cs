namespace Cadenza.Web.Components.Forms.Album;

public class EditAlbumBase : FormBase<AlbumDetailsVM>
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<EditableAlbumState> State { get; set; }

    public AlbumUpdateVM Update { get; set; }
    public AlbumDetailsVM EditableItem => Update.UpdatedItem;
    public List<AlbumTrackVM> AlbumTracks => State.Value.Tracks;

    protected override void OnInitialized()
    {
        SubscribeToAction<AlbumUpdatedAction>(OnAlbumUpdated);
        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        Dispatcher.Dispatch(new FetchEditableAlbumTracksRequest(Model.Id));
        Update = new AlbumUpdateVM(Model);
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
        Dispatcher.Dispatch(new ResetEditableAlbumTracksRequest());
        Submit();
    }

    protected void OnCancel()
    {
        Dispatcher.Dispatch(new ResetEditableAlbumTracksRequest());
        Cancel();
    }
}
