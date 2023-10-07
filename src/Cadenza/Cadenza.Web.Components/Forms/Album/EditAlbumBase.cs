namespace Cadenza.Web.Components.Forms.Album;

public class EditAlbumBase : FormBase<AlbumDetailsVM>
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<EditableAlbumState> State { get; set; }

    public EditableAlbum EditableItem => GetEditableItem();

    private EditableAlbum GetEditableItem()
    {
        return new EditableAlbum
        {
            Id = Model.Id,
            ArtistId = Model.ArtistId,
            ArtistName = Model.ArtistName,
            Title = Model.Title,
            ReleaseType = Model.ReleaseType,
            Year = Model.Year,
            ArtworkBase64 = Model.ArtworkBase64,
            Tags = Model.Tags.ToList()
        };
    }

    public List<AlbumTrackVM> AlbumTracks => State.Value.Tracks.ToList();

    protected override void OnInitialized()
    {
        SubscribeToAction<AlbumUpdatedAction>(OnAlbumUpdated);
        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        Dispatcher.Dispatch(new FetchEditableAlbumTracksRequest(Model.Id));
    }

    protected void OnSubmit()
    {
        Dispatcher.Dispatch(new AlbumUpdateRequest(Model, EditableItem));
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
