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
    public IState<CurrentTrackState> CurrentTrackState { get; set; }

    [Parameter]
    public Disc Model { get; set; } = new();

    [Parameter]
    public int AlbumId { get; set; }

    [Parameter]
    public int AlbumArtistId { get; set; }

    protected int? CurrentTrackId { get; set; }

    protected override void OnInitialized()
    {
        CurrentTrackState.StateChanged += CurrentTrackState_StateChanged;
        
        // Needed when implementing FluxorComponent
        base.OnInitialized();
    }

    private void CurrentTrackState_StateChanged(object sender, EventArgs e)
    {
        UpdateCurrentTrack();
    }

    protected override void OnParametersSet()
    {
        UpdateCurrentTrack();
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

    private void UpdateCurrentTrack()
    {
        var currentTrackId = CurrentTrackState.Value.FullTrack?.Track.Id;

        var isOldCurrentTrackOnDisc = CurrentTrackId != null;
        var isNewCurrentTrackOnDisc = currentTrackId != null && Model.Tracks.Any(t => t.TrackId == currentTrackId);

        CurrentTrackId = isNewCurrentTrackOnDisc
            ? currentTrackId
            : null;

        if (isNewCurrentTrackOnDisc || isOldCurrentTrackOnDisc)
            StateHasChanged();
    }
}
