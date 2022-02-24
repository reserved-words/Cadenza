using Cadenza.Core.Playlists;

namespace Cadenza.Core.CurrentlyPlaying;

public delegate Task PlaylistEventHandler(object sender, PlaylistEventArgs e);

public class PlaylistEventArgs : EventArgs
{
    public PlaylistId Playlist { get; set; }
    public string Error { get; set; }
}
