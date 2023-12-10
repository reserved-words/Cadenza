namespace Cadenza.Web.Components.Tabs.Edit;

public class EditArtistTabBase : FluxorComponent
{
    [Inject] public IState<EditArtistState> EditArtistState { get; set; }
    [Inject] public IChangeDetector ChangeDetector { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IEditItemMapper Mapper { get; set; }

    public bool Loading => EditArtistState.Value.IsLoading;
    public ArtistDetailsVM Artist => EditArtistState.Value.Artist;
    public IReadOnlyCollection<AlbumVM> Releases => EditArtistState.Value.Releases;

    protected EditableArtist EditableArtist { get; set; }
    protected List<EditableArtistRelease> EditableReleases { get; set; }

    protected override void OnInitialized()
    {
        SubscribeToAction<FetchEditArtistResult>(OnEditArtistFetched);
        SubscribeToAction<SaveEditItemRequest>(OnSave);
        base.OnInitialized();
    }

    private void OnSave(SaveEditItemRequest request)
    {
        if (Artist == null)
            return;

        var editedArtist = Mapper.MapEditedArtist(EditableArtist);

        if (!ChangeDetector.HasArtistChanged(Artist, editedArtist))
        {
            Dispatcher.Dispatch(new NotificationInformationRequest("No changes made"));
            Dispatcher.Dispatch(new CancelEditItemRequest());
        }
        else
        {
            Dispatcher.Dispatch(new ArtistUpdateRequest(editedArtist));
        }

        if (Releases.Any())
        {
            // TODO: Update artist releases IF any changed
            Dispatcher.Dispatch(new NotificationInformationRequest("Updating artist releases is not yet implemented"));
        }
    }

    private void OnEditArtistFetched(FetchEditArtistResult result)
    {
        if (Artist == null)
            return;

        EditableArtist = Mapper.MapEditableArtist(Artist);
        EditableReleases = Mapper.MapEditableArtistReleases(Releases);
    }
}
