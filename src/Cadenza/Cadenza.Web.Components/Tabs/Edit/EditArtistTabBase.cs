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
        var editedReleases = Mapper.MapEditedArtistReleases(EditableReleases);

        var hasArtistChanged = ChangeDetector.HasArtistChanged(Artist, editedArtist);
        var haveArtistReleasesChanged = ChangeDetector.HaveArtistReleasesChanged(Releases, editedReleases, out var changedReleases);

        if (!hasArtistChanged && !haveArtistReleasesChanged)
        {
            Dispatcher.Dispatch(new NotificationInformationRequest("No changes made"));
            Dispatcher.Dispatch(new CancelEditItemRequest());
        }
        else
        {
            var updatedArtist = hasArtistChanged ? editedArtist : null;
            Dispatcher.Dispatch(new ArtistUpdateRequest(Artist.Id, updatedArtist, changedReleases));
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
