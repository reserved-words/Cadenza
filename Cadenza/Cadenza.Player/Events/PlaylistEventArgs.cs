namespace Cadenza.Player;

public class PlaylistEventArgs : EventArgs
{
    public string PlaylistName { get; set; }
    public PlaylistType PlaylistType { get; set; }
}