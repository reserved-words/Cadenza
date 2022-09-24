namespace Cadenza.Web.Core.Player;

public delegate Task TrackFinishedEventHandler(object sender, TrackFinishedEventArgs e);

public class TrackFinishedEventArgs : EventArgs
{
}