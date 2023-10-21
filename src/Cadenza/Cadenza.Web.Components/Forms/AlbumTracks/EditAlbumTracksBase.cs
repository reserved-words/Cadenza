namespace Cadenza.Web.Components.Forms.Album;

using System.Collections.ObjectModel;
using IDialogService = Interfaces.IDialogService;

public class EditAlbumTracksBase : FormBase<AlbumDetailsVM> // Change this so model is the list of album tracks
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IDialogService DialogService { get; set; }
    [Inject] public IState<CurrentTrackState> CurrentTrackState { get; set; }

    protected List<EditableAlbumTrack> EditableItems { get; set; }

    private IReadOnlyCollection<AlbumTrackVM> _originalTracks { get; set; }

    protected override void OnInitialized()
    {
        SubscribeToAction<FetchEditableAlbumTracksResultAction>(OnEditableAlbumTracksFetched);
        SubscribeToAction<AlbumTracksUpdatedAction>(OnAlbumTracksUpdated);
        base.OnInitialized();
    }

    private void OnEditableAlbumTracksFetched(FetchEditableAlbumTracksResultAction action)
    {
        _originalTracks = action.Tracks;

        EditableItems = action.Tracks
            .Select(t => new EditableAlbumTrack
            {
                TrackId = t.TrackId,
                ArtistId = t.ArtistId,
                ArtistName = t.ArtistName,
                IdFromSource = t.IdFromSource,
                DurationSeconds = t.DurationSeconds,
                DiscNo = t.DiscNo,
                TrackNo = t.TrackNo,
                Title = t.Title
            })
            .ToList();
    }

    protected override void OnParametersSet()
    {
        Dispatcher.Dispatch(new FetchEditableAlbumTracksRequest(Model.Id));
    }

    protected void OnCancel()
    {
        Dispatcher.Dispatch(new ResetEditableAlbumTracksRequest());
        Cancel();
    }

    protected void OnSubmit()
    {
        var updatedTracks = EditableItems.Select(t => new AlbumTrackVM
        {
             TrackId = t.TrackId,
             ArtistId = t.ArtistId,
             ArtistName = t.ArtistName,
             IdFromSource = t.IdFromSource,
             DurationSeconds = t.DurationSeconds,
             DiscNo = t.DiscNo,
             TrackNo = t.TrackNo,
             Title = t.Title
        }).ToList();

        Dispatcher.Dispatch(new AlbumTracksUpdateRequest(Model.Id, _originalTracks, updatedTracks));
    }

    private void OnAlbumTracksUpdated(AlbumTracksUpdatedAction action)
    {
        Dispatcher.Dispatch(new ResetEditableAlbumTracksRequest());
        Submit();
    }

    protected void OnRemoveTrack(EditableAlbumTrack track)
    {
        EditableItems.Remove(track);
        StateHasChanged();
    }
}
