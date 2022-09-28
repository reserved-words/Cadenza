namespace Cadenza.Web.Core.Events;

public delegate Task TrackFinishedEventHandler(object sender, TrackFinishedEventArgs e);

public class TrackFinishedEventArgs : EventArgs
{
}