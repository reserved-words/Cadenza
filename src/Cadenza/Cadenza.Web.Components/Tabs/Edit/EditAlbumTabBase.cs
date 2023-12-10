namespace Cadenza.Web.Components.Tabs.Edit;

public class EditAlbumTabBase : FluxorComponent
{
    [Inject] public IState<EditAlbumState> EditAlbumState { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IEditItemMapper Mapper { get; set; }

    public bool Loading => EditAlbumState.Value.IsLoading;
    public AlbumDetailsVM Album => EditAlbumState.Value.Album;
    public IReadOnlyCollection<AlbumDiscVM> Tracks => EditAlbumState.Value.Tracks;

    protected EditableAlbum EditableAlbum { get; set; }
    protected List<EditableAlbumDisc> EditableTracks { get; set; }

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

        Dispatcher.Dispatch(new AlbumUpdateRequest(editedAlbum));
        Dispatcher.Dispatch(new AlbumTracksUpdateRequest(Album.Id, Tracks, editedTracks));
    }

    private void OnEditAlbumFetched(FetchEditAlbumResult result)
    {
        if (Album == null)
            return;

        EditableAlbum = Mapper.MapEditableAlbum(Album);
        EditableTracks = Mapper.MapEditableAlbumTracks(Tracks);
    }
}
