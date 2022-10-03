namespace Cadenza.Web.Common.Events;

public class ArtistUpdatedEventArgs : EventArgs
{
    public ArtistUpdatedEventArgs(ArtistUpdate update)
    {
        Update = update;
    }

    public ArtistUpdate Update { get; }
}
