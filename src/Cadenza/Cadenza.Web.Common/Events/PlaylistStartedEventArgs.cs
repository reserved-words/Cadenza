namespace Cadenza.Web.Common.Events;

public class PlaylistStartedEventArgs : EventArgs
{
    public PlaylistId Playlist { get; set; }
}
