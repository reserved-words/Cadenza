using Cadenza.State.Actions;
using Cadenza.State.Store;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace Cadenza.Components.Library;

public class AlbumDiscBase : FluxorComponent
{
    [Inject]
    public IDispatcher Dispatcher { get; set; }

    [Inject]
    public IMessenger Messenger { get; set; }

    [Inject]
    public IState<PlayStatusState> PlayStatusState { get; set; }

    [Parameter]
    public Disc Model { get; set; } = new();

    [Parameter]
    public int AlbumId { get; set; }

    [Parameter]
    public int AlbumArtistId { get; set; }

    protected int? CurrentTrackId { get; set; }

    protected override void OnInitialized()
    {
        // TODO - make sure that this event is raised when a playlist finishes so that the currently playing track is set back to null
        // CHECK - FluxorComponent dispose method will sort unregistering this event listener
        PlayStatusState.StateChanged += PlayStatusState_StateChanged;
    }

    private void PlayStatusState_StateChanged(object sender, EventArgs e)
    {
        if (PlayStatusState.Value.Status == PlayStatus.Playing)
        {
            UpdateCurrentTrack(PlayStatusState.Value.Track?.Id);
        }
    }

    protected override void OnParametersSet()
    {
        UpdateCurrentTrack(PlayStatusState.Value.Track?.Id);
    }

    protected Task OnDoubleClick(AlbumTrack track)
    {
        Dispatcher.Dispatch(new PlayAlbumRequest(AlbumId, track.TrackId));
        return Task.CompletedTask;
    }

    protected string CellClass(AlbumTrack track, bool isInt)
    {
        return (isInt ? "td-int " : "")
            + (track.TrackId == CurrentTrackId ? "td-highlight" : "");
    }

    protected bool IsPlaying(AlbumTrack track)
    {
        return track.TrackId == CurrentTrackId;
    }

    protected bool IsTrackArtistSameAsAlbumArtist(AlbumTrack track)
    {
        return track.ArtistId == AlbumArtistId;
    }

    private void UpdateCurrentTrack(int? currentTrackId)
    {
        var isOldCurrentTrackOnDisc = CurrentTrackId != null;
        var isNewCurrentTrackOnDisc = currentTrackId != null && Model.Tracks.Any(t => t.TrackId == currentTrackId);

        CurrentTrackId = isNewCurrentTrackOnDisc
            ? currentTrackId
            : null;

        if (isNewCurrentTrackOnDisc || isOldCurrentTrackOnDisc)
            StateHasChanged();
    }
}
