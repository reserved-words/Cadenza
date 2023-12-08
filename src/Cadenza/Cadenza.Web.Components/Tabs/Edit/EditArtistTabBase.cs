﻿namespace Cadenza.Web.Components.Tabs.Edit;

public class EditArtistTabBase : FluxorComponent
{
    [Inject] public IState<EditArtistState> EditArtistState { get; set; }
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

        Dispatcher.Dispatch(new NotificationErrorRequest("Save artist not implemented yet", null, null));
    }

    private void OnEditArtistFetched(FetchEditArtistResult result)
    {
        if (Artist == null)
            return;

        EditableArtist = Mapper.MapArtist(Artist);
        EditableReleases = Mapper.MapArtistReleases(Releases);
    }
}
