namespace Cadenza.Web.Common.Events;

public class ArtworkUpdatedEventArgs : EventArgs
{
    public ArtworkUpdatedEventArgs(AlbumUpdate update)
    {
        Update = update;
    }

    public AlbumUpdate Update { get; }
}