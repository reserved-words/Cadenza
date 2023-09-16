using Cadenza.State.Store;
using Cadenza.Web.Common.Interfaces.Play;
using Cadenza.Web.Common.Interfaces.Store;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace Cadenza.Components.Library;

public class AlbumDiscBase : FluxorComponent
{
    [Inject]
    public IItemPlayer ItemPlayer { get; set; }

    [Inject]
    public IMessenger Messenger { get; set; }

    [Inject]
    public ICurrentTrackStore Store { get; set; }

    [Inject]
    public IState<PlayStatusState> PlayStatusState { get; set; }

    [Parameter]
    public Disc Model { get; set; } = new();

    [Parameter]
    public int AlbumId { get; set; }

    [Parameter]
    public int AlbumArtistId { get; set; }

    protected int? CurrentTrackId { get; set; }

    private Guid _playlistFinishedSubscriptionId = Guid.Empty;
    //private Guid _playStatusUpdatedSubscriptionId = Guid.Empty;

    protected override void OnInitialized()
    {
        //Messenger.Subscribe<PlayStatusEventArgs>(OnPlayStatusChanged, out _playStatusUpdatedSubscriptionId);
        Messenger.Subscribe<PlaylistFinishedEventArgs>(OnPlaylistFinished, out _playlistFinishedSubscriptionId);
        PlayStatusState.StateChanged += PlayStatusState_StateChanged;
    }

    private void PlayStatusState_StateChanged(object sender, EventArgs e)
    {
        if (PlayStatusState.Value.Status == PlayStatus.Playing)
        {
            UpdateCurrentTrack(PlayStatusState.Value.Track.Id);
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        var currentTrackId = await Store.GetCurrentTrackId();
        UpdateCurrentTrack(currentTrackId);
    }

    protected async Task OnDoubleClick(AlbumTrack track)
    {
        await ItemPlayer.PlayAlbum(AlbumId, track.TrackId);
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

    private Task OnPlaylistFinished(object sender, PlaylistFinishedEventArgs args)
    {
        UpdateCurrentTrack(null);
        return Task.CompletedTask;
    }

    //private Task OnPlayStatusChanged(object sender, PlayStatusEventArgs args)
    //{
    //    if (args.Status == PlayStatus.Playing)
    //    {
    //        UpdateCurrentTrack(args.Track?.Id);
    //    }

    //    return Task.CompletedTask;
    //}

    private void UpdateCurrentTrack(int? currentTrackId)
    {
        // CHECK - should this be another state / in a rerducer / effect / somewhere else?
        // Could just be doing CurrentTrackId => State.Track.Id and that would take care of all of this anyway?

        var isOldCurrentTrackOnDisc = CurrentTrackId != null;
        var isNewCurrentTrackOnDisc = currentTrackId != null && Model.Tracks.Any(t => t.TrackId == currentTrackId);

        CurrentTrackId = isNewCurrentTrackOnDisc
            ? currentTrackId
            : null;

        if (isNewCurrentTrackOnDisc || isOldCurrentTrackOnDisc)
            StateHasChanged();
    }

    public void Dispose()
    {
        // CHECK - don't need to worry about event listener for state changed?

        //if (_playStatusUpdatedSubscriptionId != Guid.Empty)
        //{
        //    Messenger.Unsubscribe<PlayStatusEventArgs>(_playStatusUpdatedSubscriptionId);
        //}

        PlayStatusState.StateChanged -= PlayStatusState_StateChanged;

        if (_playlistFinishedSubscriptionId != Guid.Empty)
        {
            Messenger.Unsubscribe<PlaylistFinishedEventArgs>(_playlistFinishedSubscriptionId);
        }
    }
}
