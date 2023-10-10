namespace Cadenza.Web.Components.Forms.Album;

using IDialogService = Interfaces.IDialogService;
using Cadenza.Web.Components.Forms.AlbumTracks;

public class EditAlbumTracksBase : FormBase<AlbumDetailsVM>
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IDialogService DialogService { get; set; }
    [Inject] public IState<EditableAlbumState> State { get; set; }
    [Inject] public IState<CurrentTrackState> CurrentTrackState { get; set; }

    protected IReadOnlyCollection<AlbumTrackVM> Tracks => State.Value.Tracks;

    protected override void OnParametersSet()
    {
        Dispatcher.Dispatch(new FetchEditableAlbumTracksRequest(Model.Id));
    }

    public List<AlbumTrackVM> AlbumTracks => State.Value.Tracks.ToList();

    protected void OnSubmit()
    {
        Submit();
    }

    protected async Task OnRemoveTrack(AlbumTrackVM track)
    {
        var currentTrackId = CurrentTrackState.Value.Track?.Id;
        if (currentTrackId == track.TrackId)
        {
            Dispatcher.Dispatch(new NotificationErrorRequest("Track cannot be removed while currently playing", null, null));
            return;
        }

        var trackToRemove = new TrackToRemove
        {
            Id = track.TrackId,
            Title = track.Title,
            ArtistName = track.ArtistName
        };

        await DialogService.DisplayForm<RemoveTrack, TrackToRemove>(trackToRemove, "Remove Track", false);
    }
}
