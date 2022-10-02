namespace Cadenza.Web.Common.Events;

public class PlaylistFinishedEventArgs : EventArgs
{
    public PlaylistId Playlist { get; set; }
}
