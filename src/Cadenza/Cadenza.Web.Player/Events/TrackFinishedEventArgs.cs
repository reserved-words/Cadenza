namespace Cadenza.Web.Player.Events;

internal delegate Task TrackFinishedEventHandler(object sender, TrackFinishedEventArgs e);

internal class TrackFinishedEventArgs : EventArgs
{
}