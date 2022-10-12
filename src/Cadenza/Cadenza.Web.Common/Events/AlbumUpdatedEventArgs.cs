namespace Cadenza.Web.Common.Events;

public class AlbumUpdatedEventArgs : EventArgs
{
    public AlbumUpdatedEventArgs(AlbumUpdate update)
    {
        Update = update;
    }

    public AlbumUpdate Update { get; }
}
