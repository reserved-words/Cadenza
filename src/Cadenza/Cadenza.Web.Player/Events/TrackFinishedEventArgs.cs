namespace Cadenza.Web.Player;

internal delegate Task TrackFinishedEventHandler(object sender, TrackFinishedEventArgs e);

internal class TrackFinishedEventArgs : EventArgs
{
}