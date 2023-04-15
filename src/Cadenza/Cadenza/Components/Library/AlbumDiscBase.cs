using Cadenza.Web.Common.Interfaces.Play;
using Cadenza.Web.Common.Interfaces.Store;

namespace Cadenza.Components.Library;

public class AlbumDiscBase : ComponentBase
{
    [Inject]
    public IItemPlayer ItemPlayer { get; set; }

    [Inject]
    public IMessenger Messenger { get; set; }

    [Inject]
    public ICurrentTrackStore Store { get; set; }

    [Parameter]
    public Disc Model { get; set; } = new();

    [Parameter]
    public string AlbumId { get; set; }

    [Parameter]
    public string AlbumArtistId { get; set; }

    protected string CurrentTrackId { get; set; }

    private Guid _playlistFinishedSubscriptionId = Guid.Empty;
    private Guid _playStatusUpdatedSubscriptionId = Guid.Empty;
    private Guid _trackRemovedSubscriptionId = Guid.Empty;

    protected override void OnInitialized()
    {
        Messenger.Subscribe<PlayStatusEventArgs>(OnPlayStatusChanged, out _playStatusUpdatedSubscriptionId);
        Messenger.Subscribe<PlaylistFinishedEventArgs>(OnPlaylistFinished, out _playlistFinishedSubscriptionId);
        Messenger.Subscribe<TrackRemovedEventArgs>(OnTrackRemoved, out _trackRemovedSubscriptionId);
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

    private Task OnPlayStatusChanged(object sender, PlayStatusEventArgs args)
    {
        if (args.Status == PlayStatus.Playing)
        {
            UpdateCurrentTrack(args.Track?.Id);
        }

        return Task.CompletedTask;
    }

    private Task OnTrackRemoved(object sender, TrackRemovedEventArgs args)
    {
        var trackOnDisc = Model.Tracks.SingleOrDefault(t => t.TrackId == args.TrackId);
        if (trackOnDisc == null)
            return Task.CompletedTask;

        Model.Tracks.Remove(trackOnDisc);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private void UpdateCurrentTrack(string currentTrackId)
    {
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
        if (_playStatusUpdatedSubscriptionId != Guid.Empty)
        {
            Messenger.Unsubscribe<PlayStatusEventArgs>(_playStatusUpdatedSubscriptionId);
        }

        if (_playlistFinishedSubscriptionId != Guid.Empty)
        {
            Messenger.Unsubscribe<PlaylistFinishedEventArgs>(_playlistFinishedSubscriptionId);
        }

        if (_trackRemovedSubscriptionId != Guid.Empty)
        {
            Messenger.Unsubscribe<TrackRemovedEventArgs>(_trackRemovedSubscriptionId);
        }
    }
}
