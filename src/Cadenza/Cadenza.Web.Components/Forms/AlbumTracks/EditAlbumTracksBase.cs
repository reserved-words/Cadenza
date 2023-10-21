namespace Cadenza.Web.Components.Forms.Album;

using System;
using IDialogService = Interfaces.IDialogService;

public class EditAlbumTracksBase : FormBase<AlbumDetailsVM>
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IDialogService DialogService { get; set; }
    //[Inject] public IState<EditableAlbumState> State { get; set; }
    [Inject] public IState<CurrentTrackState> CurrentTrackState { get; set; }

    protected List<EditableAlbumTrack> EditableItems { get; set; }

    protected override void OnInitialized()
    {
        SubscribeToAction<FetchEditableAlbumTracksResultAction>(OnEditableAlbumTracksFetched);

    }

    private void OnEditableAlbumTracksFetched(FetchEditableAlbumTracksResultAction action)
    {
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
        //Dispatcher.Dispatch(new ResetEditableAlbumTracksRequest());
        Cancel();
    }

    protected void OnSubmit()
    {
        // Submit();
    }

    protected void OnRemoveTrack(EditableAlbumTrack track)
    {
        EditableItems.Remove(track);
        StateHasChanged();

        //var currentTrackId = CurrentTrackState.Value.Track?.Id;
        //if (currentTrackId == track.TrackId)
        //{
        //    Dispatcher.Dispatch(new NotificationErrorRequest("Track cannot be removed while currently playing", null, null));
        //    return;
        //}

        //var trackToRemove = new TrackToRemove
        //{
        //    Id = track.TrackId,
        //    Title = track.Title,
        //    ArtistName = track.ArtistName
        //};

        //await DialogService.DisplayForm<RemoveTrack, TrackToRemove>(trackToRemove, "Remove Track", DialogWidth.Small);
    }
}
