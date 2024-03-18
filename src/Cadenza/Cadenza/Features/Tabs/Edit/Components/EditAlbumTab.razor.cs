namespace Cadenza.Features.Tabs.Edit.Components;

public class EditAlbumTabBase : FluxorComponent
{
    [Inject] public IState<EditAlbumState> EditAlbumState { get; set; }
    [Inject] public IChangeDetector ChangeDetector { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IEditItemMapper Mapper { get; set; }

    public bool Loading => EditAlbumState.Value.IsLoading;
    public AlbumDetailsVM Album => EditAlbumState.Value.Album;
    public IReadOnlyCollection<AlbumTrackVM> Tracks => EditAlbumState.Value.Tracks;

    protected EditableAlbum EditableAlbum { get; set; }
    protected EditableAlbumDiscs EditableDiscs { get; set; }
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
        var editedTracks = Mapper.MapEditedAlbumTracks(EditableDiscs);

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
        EditableDiscs = Mapper.MapEditableAlbumTracks(Tracks);
    }
}
