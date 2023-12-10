namespace Cadenza.Web.Components.Tabs.Edit;

public class EditAlbumTabBase : FluxorComponent
{
    [Inject] public IState<EditAlbumState> EditAlbumState { get; set; }
    [Inject] public IChangeDetector ChangeDetector { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IEditItemMapper Mapper { get; set; }

    public bool Loading => EditAlbumState.Value.IsLoading;
    public UpdateAlbumVM Album => EditAlbumState.Value.Album;
    public IReadOnlyCollection<UpdateAlbumTrackVM> Tracks => EditAlbumState.Value.Tracks;

    protected EditableAlbum EditableAlbum { get; set; }
    protected List<EditableAlbumDisc> EditableTracks { get; set; } = new List<EditableAlbumDisc>();
    protected List<int> RemovedTracks { get; set; } = new List<int>();

    protected override void OnInitialized()
    {
        SubscribeToAction<FetchEditAlbumResult>(OnEditAlbumFetched);
        SubscribeToAction<SaveEditItemRequest>(OnSave);
        base.OnInitialized();
    }

    private void OnSave(SaveEditItemRequest request)
    {
        if (Album == null)
            return;

        var editedAlbum = Mapper.MapEditedAlbum(EditableAlbum);
        var editedTracks = Mapper.MapEditedAlbumTracks(EditableTracks);

        var hasAlbumChanged = ChangeDetector.HasAlbumChanged(Album, editedAlbum);
        var haveAlbumTracksChanged = ChangeDetector.HaveAlbumTracksChanged(Tracks, editedTracks, out var changedTracks);
        var haveAlbumTracksBeenRemoved = RemovedTracks.Any();

        if (!hasAlbumChanged && !haveAlbumTracksChanged && !haveAlbumTracksBeenRemoved)
        {
            Dispatcher.Dispatch(new NotificationInformationRequest("No changes made"));
            Dispatcher.Dispatch(new CancelEditItemRequest());
        }
        else
        {
            var updatedAlbum = hasAlbumChanged ? editedAlbum : null;
            Dispatcher.Dispatch(new AlbumUpdateRequest(Album.Id, updatedAlbum, changedTracks, RemovedTracks));
        }
    }

    private void OnEditAlbumFetched(FetchEditAlbumResult result)
    {
        if (Album == null)
            return;

        EditableAlbum = Mapper.MapEditableAlbum(Album);
        EditableTracks = Mapper.MapEditableAlbumTracks(Tracks);
    }
}
