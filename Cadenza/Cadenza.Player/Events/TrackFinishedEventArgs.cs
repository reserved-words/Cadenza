namespace Cadenza.Player;

public delegate Task TrackFinishedEventHandler(object sender, TrackFinishedEventArgs e);

public class TrackFinishedEventArgs : EventArgs
{
}