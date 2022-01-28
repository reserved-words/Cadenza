namespace Cadenza.Core;

public delegate Task PlaylistEventHandler(object sender, PlaylistEventArgs e);

public class PlaylistEventArgs : EventArgs
{
    public string PlaylistName { get; set; }
    public PlaylistType PlaylistType { get; set; }
    public string Error { get; set; }
}
