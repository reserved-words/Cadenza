namespace Cadenza.Web.Components.Forms.Album;

public class EditAlbumBase : FormBase<AlbumDetails>
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<EditableAlbumState> State { get; set; }

    public AlbumUpdate Update { get; set; }
    public AlbumDetails EditableItem => Update.UpdatedItem;
    public List<AlbumTrack> AlbumTracks => State.Value.Tracks;

    protected override void OnInitialized()
    {
        SubscribeToAction<AlbumUpdatedAction>(OnAlbumUpdated);
        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        Dispatcher.Dispatch(new FetchEditableAlbumTracksRequest(Model.Id));
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
        Dispatcher.Dispatch(new ResetEditableAlbumTracksRequest());
        Submit();
    }

    protected void OnCancel()
    {
        Dispatcher.Dispatch(new ResetEditableAlbumTracksRequest());
        Cancel();
    }
}
